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
    public class StockingServiceList : IStockingService
    {
        private DataListSingleton source;

        public StockingServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<StockingViewModel> GetList()
        {
            List<StockingViewModel> result = source.Stocking.Select(rec => new StockingViewModel
            {
                Id = rec.Id,
                StockingName = rec.StockingName
            })
            .ToList();
            return result;
        }

        public StockingViewModel GetElement(int id)
        {
            Stocking element = source.Stocking.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StockingViewModel
                {
                    Id = element.Id,
                    StockingName = element.StockingName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StockingBindingModel model)
        {
            Stocking element = source.Stocking.FirstOrDefault(rec => rec.StockingName == model.StockingName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.Stocking.Count > 0 ? source.Stocking.Max(rec => rec.Id) : 0;
            source.Stocking.Add(new Stocking
            {
                Id = maxId + 1,
                StockingName = model.StockingName
            });
        }

        public void UpdElement(StockingBindingModel model)
        {
            Stocking element = source.Stocking.FirstOrDefault(rec => rec.StockingName == model.StockingName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = source.Stocking.FirstOrDefault(rec => rec.Id == model.Id); if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StockingName = model.StockingName;
        }

        public void DelElement(int id)
        {
            Stocking element = source.Stocking.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Stocking.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
