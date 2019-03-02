using AbstractGarmentFactoryModel;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceImplement.Implementations
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;

        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<IndentViewModel> GetList()
        {
            List<IndentViewModel> result = new List<IndentViewModel>();
            for (int i = 0; i < source.Indents.Count; ++i)
            {
                string customerFIO = string.Empty;
                for (int j = 0; j < source.Customer.Count; ++j)
                {
                    if (source.Customer[j].Id == source.Indents[i].CustomerId)
                    {
                        customerFIO = source.Customer[j].CustomerFIO;
                        break;
                    }
                }
                string fabricName = string.Empty;
                for (int j = 0; j < source.Fabric.Count; ++j)
                {
                    if (source.Fabric[j].Id == source.Indents[i].FabricId)
                    {
                        fabricName = source.Fabric[j].FabricName;
                        break;
                    }
                }
                result.Add(new IndentViewModel
                {
                    Id = source.Indents[i].Id,
                    CustomerId = source.Indents[i].CustomerId,
                    CustomerFIO = customerFIO,
                    FabricId = source.Indents[i].FabricId,
                    FabricName = fabricName,
                    Amount = source.Indents[i].Amount,
                    Total = source.Indents[i].Total,
                    DateCreate = source.Indents[i].DateCreate.ToLongDateString(),
                    DateImplement = source.Indents[i].DateImplement?.ToLongDateString(),
                    Condition = source.Indents[i].Condition.ToString()
                });
            }
            return result;
        }

        public void CreateIndent(IndentBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Indents.Count; ++i)
            {
                if (source.Indents[i].Id > maxId)
                {
                    maxId = source.Customer[i].Id;
                }
            }
            source.Indents.Add(new Indent
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                FabricId = model.FabricId,
                DateCreate = DateTime.Now,
                Amount = model.Amount,
                Total = model.Total,
                Condition = IndentStatus.Принят
            });
        }

        public void TakeIndentInWork(IndentBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Indents.Count; ++i)
            {
                if (source.Indents[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Indents[index].Condition != IndentStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            source.Indents[index].DateImplement = DateTime.Now;
            source.Indents[index].Condition = IndentStatus.Выполняется;
        }

        public void FinishIndent(IndentBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Indents.Count; ++i)
            {
                if (source.Customer[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Indents[index].Condition != IndentStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            source.Indents[index].Condition = IndentStatus.Готов;
        }

        public void PayIndent(IndentBindingModel model)
        {
            int index = -1; for (int i = 0; i < source.Indents.Count; ++i)
            {
                if (source.Customer[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Indents[index].Condition != IndentStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            source.Indents[index].Condition = IndentStatus.Оплачен;
        }
    }
}
