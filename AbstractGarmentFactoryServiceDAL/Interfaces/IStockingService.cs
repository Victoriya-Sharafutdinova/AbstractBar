using AbstractGarmentFactoryServiceDAL.Attributies;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с компонентами")]
    public interface IStockingService
    {
        [CustomMethod("Метод получения списка компонентов")]
        List<StockingViewModel> GetList();

        [CustomMethod("Метод получения компонента по id")]
        StockingViewModel GetElement(int id);

        [CustomMethod("Метод добавления компонента")]
        void AddElement(StockingBindingModel model);

        [CustomMethod("Метод изменения данных по компоненту")]
        void UpdElement(StockingBindingModel model);

        [CustomMethod("Метод удаления компонента")]
        void DelElement(int id);
    }
}
