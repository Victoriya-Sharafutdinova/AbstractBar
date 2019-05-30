using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Attributies;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IMainService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<IndentViewModel> GetList();

        [CustomMethod("Метод получения списка невыполненных заказов")]
        List<IndentViewModel> GetFreeIndents();

        [CustomMethod("Метод создания заказа")]
        void CreateIndent(IndentBindingModel model);

        [CustomMethod("Метод отправки заказа в работу")]
        void TakeIndentInWork(IndentBindingModel model);

        [CustomMethod("Метод завершения выполнения заказа")]
        void FinishIndent(IndentBindingModel model);

        [CustomMethod("Метод оплаты заказа")]
        void PayIndent(IndentBindingModel model);

        [CustomMethod("Метод пополнения склада")]
        void PutStockingOnStorage(StorageStockingBindingModel model);
    }
}
