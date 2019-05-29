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
    [CustomInterface("Интерфейс для работы с продуктами")]
    public interface IFabricService
    {
        [CustomMethod("Метод получения списка продуктов")]
        List<FabricViewModel> GetList();

        [CustomMethod("Метод получения продукта по id")]
        FabricViewModel GetElement(int id);

        [CustomMethod("Метод добавления продукта")]
        void AddElement(FabricBindingModel model);

        [CustomMethod("Метод изменения данных по продукту")]
        void UpdElement(FabricBindingModel model);

        [CustomMethod("Метод удаления продукта")]
        void DelElement(int id);
    }
}
