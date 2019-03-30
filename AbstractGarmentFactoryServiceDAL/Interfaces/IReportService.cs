using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.Interfaces
{
    public interface IReportService
    {
        void SaveFabricValue(ReportBindingModel model);

        List<StorageLoadViewModel> GetStoragesLoad();

        void SaveStoragesLoad(ReportBindingModel model);

        List<CustomerIndentsModel> GetCustomerIndents(ReportBindingModel model);

        void SaveCustomerIndents(ReportBindingModel model);
    }
}
