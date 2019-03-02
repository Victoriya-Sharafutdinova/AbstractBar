using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.Interfaces
{
    public interface IFabricService
    {
        List<FabricViewModel> GetList();

        FabricViewModel GetElement(int id);

        void AddElement(FabricBindingModel model);

        void UpdElement(FabricBindingModel model);

        void DelElement(int id);
    }
}
