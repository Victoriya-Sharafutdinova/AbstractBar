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
    public class FabricServiceList : IFabricService
    {
        private DataListSingleton source;

        public FabricServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<FabricViewModel> GetList()
        {
            List<FabricViewModel> result = source.Fabric.Select(rec => new FabricViewModel
            {
                Id = rec.Id,
                FabricName = rec.FabricName,
                Value = rec.Value,
                FabricStocking = source.FabricStocking
                    .Where(recPC => recPC.FabricId == rec.Id)
                    .Select(recPC => new FabricStockingViewModel
                    {
                        Id = recPC.Id,
                        FabricId = recPC.FabricId,
                        StockingId = recPC.StockingId,
                        StockingName = source.Stocking.FirstOrDefault(recC => recC.Id == recPC.StockingId)?.StockingName,
                        Amount = recPC.Amount
                    })
                    .ToList()
            })
            .ToList();
            return result;
        }

        public FabricViewModel GetElement(int id)
        {
            Fabric element = source.Fabric.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new FabricViewModel
                {
                    Id = element.Id,
                    FabricName = element.FabricName,
                    Value = element.Value,
                    FabricStocking = source.FabricStocking
                        .Where(recPC => recPC.FabricId == element.Id)
                        .Select(recPC => new FabricStockingViewModel
                        {
                            Id = recPC.Id,
                            FabricId = recPC.FabricId,
                            StockingId = recPC.StockingId,
                            StockingName = source.Stocking.FirstOrDefault(recC => recC.Id == recPC.StockingId)?.StockingName,
                            Amount = recPC.Amount
                        })
                        .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(FabricBindingModel model)
        {
            Fabric element = source.Fabric.FirstOrDefault(rec => rec.FabricName == model.FabricName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Fabric.Count > 0 ? source.Fabric.Max(rec => rec.Id) : 0;
            source.Fabric.Add(new Fabric
            {
                Id = maxId + 1,
                FabricName = model.FabricName,
                Value = model.Value
            });
            // компоненты для изделия        
            int maxPCId = source.FabricStocking.Count > 0 ? source.FabricStocking.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам         
            var GroupStocking = model.FabricStocking
                .GroupBy(rec => rec.StockingId)
                .Select(rec => new
                {
                    StockingId = rec.Key,
                    Amount = rec.Sum(r => r.Amount)
                });
            // добавляем компоненты             
            foreach (var groupStocking in GroupStocking)
            {
                source.FabricStocking.Add(new FabricStocking
                {
                    Id = ++maxPCId,
                    FabricId = maxId + 1,
                    StockingId = groupStocking.StockingId,
                    Amount = groupStocking.Amount
                });
            }
        }

        public void UpdElement(FabricBindingModel model)
        {
            Fabric element = source.Fabric.FirstOrDefault(rec => rec.FabricName == model.FabricName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Fabric.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.FabricName = model.FabricName;
            element.Value = model.Value;

            int maxPCId = source.FabricStocking.Count > 0 ? source.FabricStocking.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты    
            var compIds = model.FabricStocking.Select(rec => rec.StockingId).Distinct();
            var UpdateStocking = source.FabricStocking.Where(rec => rec.FabricId == model.Id && compIds.Contains(rec.StockingId));
            foreach (var updateStocking in UpdateStocking)
            {
                updateStocking.Amount = model.FabricStocking.FirstOrDefault(rec => rec.Id == updateStocking.Id).Amount;
            }
            source.FabricStocking.RemoveAll(rec => rec.FabricId == model.Id && !compIds.Contains(rec.StockingId));
            // новые записи         
            var GroupStocking = model.FabricStocking
                .Where(rec => rec.Id == 0)
                .GroupBy(rec => rec.StockingId)
                .Select(rec => new
                {
                    StockingId = rec.Key,
                    Amount = rec.Sum(r => r.Amount)
                });
            foreach (var groupStocking in GroupStocking)
            {
                FabricStocking elementPC = source.FabricStocking.FirstOrDefault(rec => rec.FabricId == model.Id && rec.StockingId == groupStocking.StockingId);
                if (elementPC != null)
                {
                    elementPC.Amount += groupStocking.Amount;
                }
                else
                {
                    source.FabricStocking.Add(new FabricStocking
                    {
                        Id = ++maxPCId,
                        FabricId = model.Id,
                        StockingId = groupStocking.StockingId,
                        Amount = groupStocking.Amount
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            Fabric element = source.Fabric.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия 
                source.FabricStocking.RemoveAll(rec => rec.FabricId == id);
                source.Fabric.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
