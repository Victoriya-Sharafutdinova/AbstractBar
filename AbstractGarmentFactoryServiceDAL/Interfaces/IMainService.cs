using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<IndentViewModel> GetList();

        void CreateIndent(IndentBindingModel model);

        void TakeIndentInWork(IndentBindingModel model);

        void FinishIndent(IndentBindingModel model);

        void PayIndent(IndentBindingModel model);

        void PutStockingOnStorage(StorageStockingBindingModel model);
    }
}
