using System;
using System.Collections.Generic;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractGarmentFactoryServiceDAL.Attributies;

namespace AbstractGarmentFactoryServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с исполнителями")]
    public interface IImplementerService
    {
        [CustomMethod("Метод получения списка исполнителей")]
        List<ImplementerViewModel> GetList();

        [CustomMethod("Метод получения исполнителя по id")]
        ImplementerViewModel GetElement(int id);

        [CustomMethod("Метод добавления исполнителя")]
        void AddElement(ImplementerBindingModel model);

        [CustomMethod("Метод изменения данных по исполнителю")]
        void UpdElement(ImplementerBindingModel model);

        [CustomMethod("Метод удаления исполнителя")]
        void DelElement(int id);

        [CustomMethod("Метод получения свобоного исполнителя")]
        ImplementerViewModel GetFreeWorker();
    }
}
