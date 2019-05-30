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
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [CustomMethod("Метод сохранения списка продуктов")]
        void SaveFabricValue(ReportBindingModel model);

        [CustomMethod("Метод получения списка складов и компонентов")]
        List<StorageLoadViewModel> GetStoragesLoad();

        [CustomMethod("Метод сохранения списка складов")]
        void SaveStoragesLoad(ReportBindingModel model);

        [CustomMethod("Метод получения списка заказов клиентов")]
        List<CustomerIndentsModel> GetCustomerIndents(ReportBindingModel model);

        [CustomMethod("Метод сохранения списка заказов клиентов")]
        void SaveCustomerIndents(ReportBindingModel model);
    }
}
