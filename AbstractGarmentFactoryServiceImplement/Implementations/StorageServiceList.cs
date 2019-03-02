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
    public class StorageServiceList : IStorageService
    {
        private DataListSingleton source;

        public StorageServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = source.Storage.Select(rec => new StorageViewModel
            {
                Id = rec.Id,
                StorageName = rec.StorageName,
                StorageStocking = source.StorageStocking
                    .Where(recPC => recPC.StorageId == rec.Id)
                    .Select(recPC => new StorageStockingViewModel
                    {
                        Id = recPC.Id,
                        StorageId = recPC.StorageId,
                        StockingId = recPC.StockingId,
                        StockingName = source.Stocking.FirstOrDefault(recC => recC.Id == recPC.StockingId)?.StockingName,
                        Amount = recPC.Amount
                    })
                    .ToList()
            })
            .ToList();
            return result;
        }

        public StorageViewModel GetElement(int id)
        {
            Storage element = source.Storage.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StorageViewModel
                {
                    Id = element.Id,
                    StorageName = element.StorageName,
                    StorageStocking = source.StorageStocking
                        .Where(recPC => recPC.StorageId == element.Id)
                        .Select(recPC => new StorageStockingViewModel
                        {
                            Id = recPC.Id,
                            StorageId = recPC.StorageId,
                            StockingId = recPC.StockingId,
                            StockingName = source.Stocking.FirstOrDefault(recC => recC.Id == recPC.StockingId)?.StockingName,
                            Amount = recPC.Amount
                        })
                        .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StorageBindingModel model)
        {
            Storage element = source.Storage.FirstOrDefault(rec => rec.StorageName == model.StorageName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Storage.Count > 0 ? source.Storage.Max(rec => rec.Id) : 0;
            source.Storage.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }

        public void UpdElement(StorageBindingModel model)
        {
            Storage element = source.Storage.FirstOrDefault(rec => rec.StorageName == model.StorageName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = source.Storage.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StorageName = model.StorageName;
        }

        public void DelElement(int id)
        {
            Storage element = source.Storage.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // при удалении удаляем все записи о компонентах на удаляемом складе     
                source.StorageStocking.RemoveAll(rec => rec.StorageId == id);
                source.Storage.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
