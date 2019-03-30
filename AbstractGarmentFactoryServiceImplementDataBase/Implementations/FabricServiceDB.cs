using AbstractGarmentFactoryModel;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceImplementDataBase.Implementations
{
    public class FabricServiceDB : IFabricService
    {
        private AbstractDBScope context;

        public FabricServiceDB(AbstractDBScope context)
        {
            this.context = context;
        }

        public List<FabricViewModel> GetList()
        {
            List<FabricViewModel> result = context.Fabrics.Select(rec => new FabricViewModel
            {
                Id = rec.Id,
                FabricName = rec.FabricName,
                Value = rec.Value,
                FabricStocking = context.FabricStockings
                .Where(recPC => recPC.FabricId == rec.Id)
                .Select(recPC => new FabricStockingViewModel
                {
                    Id = recPC.Id,
                    FabricId = recPC.FabricId,
                    StockingId = recPC.StockingId,
                    StockingName = recPC.Stocking.StockingName,
                    Amount = recPC.Amount
                }).ToList()
            }).ToList();
            return result;
        }

        public FabricViewModel GetElement(int id)
        {
            Fabric element = context.Fabrics.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new FabricViewModel
                {
                    Id = element.Id,
                    FabricName = element.FabricName,
                    Value = element.Value,
                    FabricStocking = context.FabricStockings.Where(recPC => recPC.FabricId == element.Id).Select(recPC => new FabricStockingViewModel
                    {
                        Id = recPC.Id,
                        FabricId = recPC.FabricId,
                        StockingId = recPC.StockingId,
                        StockingName = recPC.Stocking.StockingName,
                        Amount = recPC.Amount
                    }).ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(FabricBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Fabric element = context.Fabrics.FirstOrDefault(rec => rec.FabricName == model.FabricName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Fabric
                    {
                        FabricName = model.FabricName,
                        Value = model.Value
                    };
                    context.Fabrics.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам     
                    var groupStockings = model.FabricStocking
                        .GroupBy(rec => rec.StockingId)              
                        .Select(rec => new
                        {
                            StockingId = rec.Key,
                            Amount = rec.Sum(r => r.Amount)
                        });
                    // добавляем компоненты     
                    foreach (var groupStocking in groupStockings)
                    {
                        context.FabricStockings.Add(new FabricStocking
                        {
                            FabricId = element.Id,
                            StockingId = groupStocking.StockingId,
                            Amount = groupStocking.Amount
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; 
                }
            }
        }

        public void UpdElement(FabricBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Fabric element = context.Fabrics.FirstOrDefault(rec => rec.FabricName == model.FabricName && rec.Id != model.Id);
                    if (element != null)
                    { throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Fabrics.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.FabricName = model.FabricName;
                    element.Value = model.Value;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты     
                    var compIds = model.FabricStocking.Select(rec => rec.StockingId).Distinct();
                    var updateStockings = context.FabricStockings.Where(rec => rec.FabricId == model.Id && compIds.Contains(rec.StockingId));
                    foreach (var updateStocking in updateStockings)
                    {
                        updateStocking.Amount = model.FabricStocking.FirstOrDefault(rec => rec.Id == updateStocking.Id).Amount;
                    }
                    context.SaveChanges();
                    context.FabricStockings.RemoveRange(context.FabricStockings.Where(rec => rec.FabricId == model.Id && !compIds.Contains(rec.StockingId)));
                    context.SaveChanges();
                    // новые записи                
                    var groupStockings = model.FabricStocking  
                        .Where(rec => rec.Id == 0)             
                        .GroupBy(rec => rec.StockingId)         
                        .Select(rec => new
                        {
                            StockingId = rec.Key,
                            Amount = rec.Sum(r => r.Amount)
                        });
                    foreach (var groupStocking in groupStockings)
                    {
                        FabricStocking elementPC = context.FabricStockings.FirstOrDefault(rec => rec.FabricId == model.Id && rec.StockingId == groupStocking.StockingId);
                        if (elementPC != null)
                        {
                            elementPC.Amount += groupStocking.Amount;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.FabricStockings.Add(new FabricStocking
                            {
                                FabricId = model.Id,
                                StockingId = groupStocking.StockingId,
                                Amount = groupStocking.Amount
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    } 
 
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Fabric element = context.Fabrics.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия 
                        context.FabricStockings.RemoveRange(context.FabricStockings.Where(rec => rec.FabricId == id));
                        context.Fabrics.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }   
    }
}
