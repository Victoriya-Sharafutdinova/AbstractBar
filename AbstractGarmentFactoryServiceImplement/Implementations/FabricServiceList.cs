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
            List<FabricViewModel> result = new List<FabricViewModel>();
            for (int i = 0; i < source.Fabric.Count; ++i)
            {
              
                List<FabricStockingViewModel> fabricStocking = new List<FabricStockingViewModel>();
                for (int j = 0; j < source.FabricStocking.Count; ++j)
                {
                    if (source.FabricStocking[j].FabricId == source.Fabric[i].Id)
                    {
                        string stockingName = string.Empty;
                        for (int k = 0; k < source.Stocking.Count; ++k)
                        {
                            if (source.FabricStocking[j].StockingId == source.Stocking[k].Id)
                            {
                                stockingName = source.Stocking[k].StockingName;
                                break;
                            }
                        }
                        fabricStocking.Add(new FabricStockingViewModel
                        {
                            Id = source.FabricStocking[j].Id,
                            FabricId = source.FabricStocking[j].FabricId,
                            StockingId = source.FabricStocking[j].StockingId,
                            StockingName = stockingName,
                            Amount = source.FabricStocking[j].Amount
                        });
                    }
                }
                result.Add(new FabricViewModel
                {
                    Id = source.Fabric[i].Id,
                    FabricName = source.Fabric[i].FabricName,
                    Value = source.Fabric[i].Value,
                    FabricStocking = fabricStocking
                });
            }
            return result;
        }

        public FabricViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Fabric.Count; ++i)
            {                 
                List<FabricStockingViewModel> fabricStocking = new List<FabricStockingViewModel>();
                for (int j = 0; j < source.FabricStocking.Count; ++j)
                {
                    if (source.FabricStocking[j].FabricId == source.Fabric[i].Id)
                    {
                        string stockingName = string.Empty;
                        for (int k = 0; k < source.Stocking.Count; ++k)
                        {
                            if (source.FabricStocking[j].StockingId == source.Stocking[k].Id)
                            {
                                stockingName = source.Stocking[k].StockingName;
                                break;
                            }
                        }
                        fabricStocking.Add(new FabricStockingViewModel
                        {
                            Id = source.FabricStocking[j].Id,
                            FabricId = source.FabricStocking[j].FabricId,
                            StockingId = source.FabricStocking[j].StockingId,
                            StockingName = stockingName,
                            Amount = source.FabricStocking[j].Amount
                        });
                    }
                }
                if (source.Fabric[i].Id == id)
                {
                    return new FabricViewModel
                    {
                        Id = source.Fabric[i].Id,
                        FabricName = source.Fabric[i].FabricName,
                        Value = source.Fabric[i].Value,
                        FabricStocking = fabricStocking
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(FabricBindingModel model)
        {
            int maxId = 0; for (int i = 0; i < source.Fabric.Count; ++i)
            {
                if (source.Fabric[i].Id > maxId)
                {
                    maxId = source.Fabric[i].Id;
                }
                if (source.Fabric[i].FabricName == model.FabricName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Fabric.Add(new Fabric
            {
                Id = maxId + 1,
                FabricName = model.FabricName,
                Value = model.Value
            });
            // компоненты для изделия             
            int maxPCId = 0;
            for (int i = 0; i < source.FabricStocking.Count; ++i)
            {
                if (source.FabricStocking[i].Id > maxPCId)
                {
                    maxPCId = source.FabricStocking[i].Id;
                }
            }
            // убираем дубли по компонентам             
            for (int i = 0; i < model.FabricStocking.Count; ++i)
            {
                for (int j = 1; j < model.FabricStocking.Count; ++j)
                {
                    if (model.FabricStocking[i].StockingId == model.FabricStocking[j].StockingId)
                    {
                        model.FabricStocking[i].Amount += model.FabricStocking[j].Amount;
                        model.FabricStocking.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты             
            for (int i = 0; i < model.FabricStocking.Count; ++i)
            {
                source.FabricStocking.Add(new FabricStocking
                {
                    Id = ++maxPCId,
                    FabricId = maxId + 1,
                    StockingId = model.FabricStocking[i].StockingId,
                    Amount = model.FabricStocking[i].Amount
                });
            }
        }

        public void UpdElement(FabricBindingModel model)
        {
            int index = -1; for (int i = 0; i < source.Fabric.Count; ++i)
            {
                if (source.Fabric[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Fabric[i].FabricName == model.FabricName && source.Fabric[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Fabric[index].FabricName = model.FabricName;
            source.Fabric[index].Value = model.Value;
            int maxPCId = 0;
            for (int i = 0; i < source.FabricStocking.Count; ++i)
            {
                if (source.FabricStocking[i].Id > maxPCId)
                {
                    maxPCId = source.FabricStocking[i].Id;
                }
            }
            // обновляем существуюущие компоненты 
            for (int i = 0; i < source.FabricStocking.Count; ++i)
            {
                if (source.FabricStocking[i].FabricId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.FabricStocking.Count; ++j)
                    {
                        // если встретили, то изменяем количество                         
                        if (source.FabricStocking[i].Id == model.FabricStocking[j].Id)
                        {
                            source.FabricStocking[i].Amount = model.FabricStocking[j].Amount;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем                     
                    if (flag)
                    {
                        source.FabricStocking.RemoveAt(i--);
                    }
                }
            }
            // новые записи             
            for (int i = 0; i < model.FabricStocking.Count; ++i)
            {
                if (model.FabricStocking[i].Id == 0)
                {
                    // ищем дубли                     
                    for (int j = 0; j < source.FabricStocking.Count; ++j)
                    {
                        if (source.FabricStocking[j].FabricId == model.Id && source.FabricStocking[j].StockingId == model.FabricStocking[i].StockingId)
                        {
                            source.FabricStocking[j].Amount += model.FabricStocking[i].Amount;
                            model.FabricStocking[i].Id = source.FabricStocking[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись                   
                    if (model.FabricStocking[i].Id == 0)
                    {
                        source.FabricStocking.Add(new FabricStocking
                        {
                            Id = ++maxPCId,
                            FabricId = model.Id,
                            StockingId = model.FabricStocking[i].StockingId,
                            Amount = model.FabricStocking[i].Amount
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия    
            for (int i = 0; i < source.FabricStocking.Count; ++i)
            {
                if (source.FabricStocking[i].FabricId == id)
                {
                    source.FabricStocking.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Fabric.Count; ++i)
            {
                if (source.Fabric[i].Id == id)
                {
                    source.Fabric.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
