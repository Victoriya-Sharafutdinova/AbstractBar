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
            List<StockingViewModel> result = new List<StockingViewModel>();
            for (int i = 0; i < source.Stocking.Count; ++i)
            {
                result.Add(new StockingViewModel
                {
                    Id = source.Stocking[i].Id,
                    StockingName = source.Stocking[i].StockingName
                });
            }

            return result;
        }

        public StockingViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Stocking.Count; ++i)
            {
                if (source.Stocking[i].Id == id)
                {
                    return new StockingViewModel
                    {
                        Id = source.Stocking[i].Id,
                        StockingName = source.Stocking[i].StockingName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StockingBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Stocking.Count; ++i)
            {
                if (source.Stocking[i].Id > maxId)
                {
                    maxId = source.Stocking[i].Id;
                }
                if (source.Stocking[i].StockingName == model.StockingName)
                {
                    throw new Exception("Уже есть такой ингредиент");
                }
            }
            source.Stocking.Add(new Stocking
            {
                Id = maxId + 1,
                StockingName = model.StockingName
            });
        }

        public void UpdElement(StockingBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Stocking.Count; ++i)
            {
                if (source.Stocking[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Stocking[i].StockingName == model.StockingName && source.Stocking[i].Id != model.Id)
                {
                    throw new Exception("Уже есть такой ингредиент");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Stocking[index].StockingName = model.StockingName;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.Stocking.Count; ++i)
            {
                if (source.Stocking[i].Id == id)
                {
                    source.Stocking.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
