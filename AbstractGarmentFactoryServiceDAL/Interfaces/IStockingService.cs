using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.Interfaces
{
    public interface IStockingService
    {
        List<StockingViewModel> GetList();

        StockingViewModel GetElement(int id);

        void AddElement(StockingBindingModel model);

        void UpdElement(StockingBindingModel model);

        void DelElement(int id);
    }
}
