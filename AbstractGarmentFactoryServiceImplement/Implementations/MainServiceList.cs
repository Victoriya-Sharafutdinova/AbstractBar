using AbstractGarmentFactoryModel;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceImplement.Implementations
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;

        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<IndentViewModel> GetList()
        {
            List<IndentViewModel> result = source.Indents
                .Select(rec => new IndentViewModel
                {
                    Id = rec.Id,
                    CustomerId = rec.CustomerId,
                    FabricId = rec.FabricId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    DateImplement = rec.DateImplement?.ToLongDateString(),
                    Condition = rec.Condition.ToString(),
                    Amount = rec.Amount,
                    Total = rec.Total,
                    CustomerFIO = source.Customer.FirstOrDefault(recC => recC.Id == rec.CustomerId)?.CustomerFIO,
                    FabricName = source.Fabric.FirstOrDefault(recP => recP.Id == rec.FabricId)?.FabricName,
                })
                .ToList();
            return result;
        }

        public void CreateIndent(IndentBindingModel model)
        {
            int maxId = source.Indents.Count > 0 ? source.Indents.Max(rec => rec.Id) : 0;
            source.Indents.Add(new Indent
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                FabricId = model.FabricId,
                DateCreate = DateTime.Now,
                Amount = model.Amount,
                Total = model.Total,
                Condition = IndentCondition.Принят
            });
        }

        public void TakeIndentInWork(IndentBindingModel model)
        {
            Indent element = source.Indents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Condition != IndentCondition.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            // смотрим по количеству компонентов на складах    
            var fabricStockings = source.FabricStocking.Where(rec => rec.FabricId == element.FabricId);
            foreach (var fabricStocking in fabricStockings)
            {
                int amountOnStorage = source.StorageStocking
                    .Where(rec => rec.StockingId == fabricStocking.StockingId)
                    .Sum(rec => rec.Amount);
                if (amountOnStorage < fabricStocking.Amount * element.Amount)
                {
                    var stockingName = source.Stocking.FirstOrDefault(rec => rec.Id == fabricStocking.StockingId);
                    throw new Exception("Не достаточно компонента " + stockingName?.StockingName + " требуется " + (fabricStocking.Amount * element.Amount) + ", в наличии " + amountOnStorage);
                }
            }
            // списываем   
            foreach (var fabricStocking in fabricStockings)
            {
                int AmountOnStorage = fabricStocking.Amount * element.Amount;
                var StorageStocking = source.StorageStocking
                    .Where(rec => rec.StockingId == fabricStocking.StockingId);
                foreach (var storageStocking in StorageStocking)
                {
                    // компонентов на одном слкаде может не хватать 
                    if (storageStocking.Amount >= AmountOnStorage)
                    {
                        storageStocking.Amount -= AmountOnStorage;
                        break;
                    }
                    else
                    {
                        AmountOnStorage -= storageStocking.Amount;
                        storageStocking.Amount = 0;
                    }
                }
            }
            element.DateImplement = DateTime.Now;
            element.Condition = IndentCondition.Выполняется;
        }

        public void FinishIndent(IndentBindingModel model)
        {
            Indent element = source.Indents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Condition != IndentCondition.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Condition = IndentCondition.Готов;
        }

        public void PayIndent(IndentBindingModel model)
        {
            Indent element = source.Indents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Condition != IndentCondition.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Condition = IndentCondition.Оплачен;
        }

        public void PutStockingOnStorage(StorageStockingBindingModel model)
        {
            StorageStocking element = source.StorageStocking.FirstOrDefault(rec => rec.StorageId == model.StorageId && rec.StockingId == model.StockingId);
            if (element != null)
            {
                element.Amount += model.Amount;
            }
            else
            {
                int maxId = source.StorageStocking.Count > 0 ? source.StorageStocking.Max(rec => rec.Id) : 0;
                source.StorageStocking.Add(new StorageStocking
                {
                    Id = ++maxId,
                    StorageId = model.StorageId,
                    StockingId = model.StockingId,
                    Amount = model.Amount
                });
            }
        }
        public List<IndentViewModel> GetFreeIndents()
        {
            List<IndentViewModel> result = source.Indents
            .Where(x => x.Condition == IndentCondition.Принят || x.Condition == IndentCondition.НедостаточноРесурсов)
            .Select(rec => new IndentViewModel
            {
                Id = rec.Id
            })
            .ToList();
            return result;
        }
    }
}
