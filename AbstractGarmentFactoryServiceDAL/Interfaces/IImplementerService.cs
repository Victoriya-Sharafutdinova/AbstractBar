using System;
using System.Collections.Generic;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.Interfaces
{
    public interface IImplementerService
    {
        List<ImplementerViewModel> GetList();

        ImplementerViewModel GetElement(int id);

        void AddElement(ImplementerBindingModel model);

        void UpdElement(ImplementerBindingModel model);

        void DelElement(int id);

        ImplementerViewModel GetFreeWorker();
    }
}
