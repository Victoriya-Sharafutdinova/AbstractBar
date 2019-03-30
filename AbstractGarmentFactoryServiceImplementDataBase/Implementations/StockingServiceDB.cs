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
    public class StockingServiceDB : IStockingService
    {
        private AbstractDBScope context;

        public StockingServiceDB(AbstractDBScope context)
        {
            this.context = context;
        }

        public List<StockingViewModel> GetList()
        {
            List<StockingViewModel> result = context.Stockings.Select(rec => new StockingViewModel
            {
                Id = rec.Id,
                StockingName = rec.StockingName
            })
            .ToList();
            return result;
        }

        public StockingViewModel GetElement(int id)
        {
            Stocking element = context.Stockings.FirstOrDefault(rec => rec.Id == id);
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
            Stocking element = context.Stockings.FirstOrDefault(rec => rec.StockingName == model.StockingName);
            if (element != null)
            {
                throw new Exception("Уже есть такое изделие");
            }
            context.Stockings.Add(new Stocking
            {
                StockingName = model.StockingName
            });
            context.SaveChanges();
        }

        public void UpdElement(StockingBindingModel model)
        {
            Stocking element = context.Stockings.FirstOrDefault(rec => rec.StockingName == model.StockingName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть такой компонент");
            }
            element = context.Stockings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StockingName = model.StockingName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Stocking element = context.Stockings.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Stockings.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
