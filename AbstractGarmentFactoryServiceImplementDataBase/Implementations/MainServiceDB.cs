using AbstractGarmentFactoryModel;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using AbstractGarmentFactoryServiceDAL.Interfaces;

namespace AbstractGarmentFactoryServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private AbstractDBScope context;

        public MainServiceDB(AbstractDBScope context)
        {
            this.context = context;
        }

        public List<IndentViewModel> GetList()
        {
            List<IndentViewModel> result = context.Indents.Select(rec => new IndentViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                FabricId = rec.FabricId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " + SqlFunctions.DateName("mm", rec.DateCreate) + " " + SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" : SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " + SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " + SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                Condition = rec.Condition.ToString(),
                Amount = rec.Amount,
                Total = rec.Total,
                CustomerFIO = rec.Customer.CustomerFIO,
                FabricName = rec.Fabric.FabricName
            }).ToList();
            return result;
        }

        public void CreateIndent(IndentBindingModel model)
        {
            context.Indents.Add(new Indent
            {
                CustomerId = model.CustomerId,
                FabricId = model.FabricId,
                DateCreate = DateTime.Now,
                Amount = model.Amount,
                Total = model.Total,
                Condition = IndentCondition.Принят
            });
            context.SaveChanges();
        }

        public void TakeIndentInWork(IndentBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Indent element = context.Indents.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Condition != IndentCondition.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var fabricStockings = context.FabricStockings.Include(rec => rec.Stocking).Where(rec => rec.FabricId == element.FabricId).ToList();
                    // списываем      
                    foreach (var fabricStocking in fabricStockings)
                    {
                        int amountOnStorage = fabricStocking.Amount * element.Amount;
                        var storageStockings = context.StorageStockings.Where(rec => rec.StockingId == fabricStocking.StockingId).ToList();
                        foreach (var storageStocking in storageStockings)
                        {
                            // компонентов на одном слкаде может не хватать  
                            if (storageStocking.Amount >= amountOnStorage)
                            {
                                storageStocking.Amount -= amountOnStorage;
                                amountOnStorage = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                amountOnStorage -= storageStocking.Amount;
                                storageStocking.Amount = 0;
                                context.SaveChanges();
                            }
                        }
                        if (amountOnStorage > 0)
                        {
                            throw new Exception("Не достаточно компонента " + fabricStocking.Stocking.StockingName + " требуется " + fabricStocking.Amount + ", не хватает " + amountOnStorage);
                        }
                    }
                    element.DateImplement = DateTime.Now;
                    element.Condition = IndentCondition.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        } 

        public void FinishIndent(IndentBindingModel model)
        {
            Indent element = context.Indents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Condition != IndentCondition.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Condition = IndentCondition.Готов;
            context.SaveChanges();
        }

        public void PayIndent(IndentBindingModel model)
        {
            Indent element = context.Indents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Condition != IndentCondition.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Condition = IndentCondition.Оплачен;
            context.SaveChanges();
        }

        public void PutStockingOnStorage(StorageStockingBindingModel model)
        {
            StorageStocking element = context.StorageStockings.FirstOrDefault(rec => rec.StorageId == model.StorageId && rec.StockingId == model.StockingId);
            if (element != null)
            {
                element.Amount += model.Amount;
            }
            else
            {
                context.StorageStockings.Add(new StorageStocking
                {
                    StorageId = model.StorageId,
                    StockingId = model.StockingId,
                    Amount = model.Amount
                });
            }
            context.SaveChanges();
        }
    }
}
