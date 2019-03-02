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
    public class CustomerServiceList : ICustomerService
    {
        private DataListSingleton source;

        public CustomerServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<CustomerViewModel> GetList()
        {
            List<CustomerViewModel> result = source.Customer.Select(rec => new CustomerViewModel
            {
                Id = rec.Id,
                CustomerFIO = rec.CustomerFIO
            })
            .ToList();
            return result;
        }

        public CustomerViewModel GetElement(int id)
        {
            Customer element = source.Customer.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CustomerViewModel
                {
                    Id = element.Id,
                    CustomerFIO = element.CustomerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CustomerBindingModel model)
        {
            Customer element = source.Customer.FirstOrDefault(rec => rec.CustomerFIO == model.CustomerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Customer.Count > 0 ? source.Customer.Max(rec => rec.Id) : 0;
            source.Customer.Add(new Customer
            {
                Id = maxId + 1,
                CustomerFIO = model.CustomerFIO
            });
        }

        public void UpdElement(CustomerBindingModel model)
        {
            Customer element = source.Customer.FirstOrDefault(rec => rec.CustomerFIO == model.CustomerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Customer.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CustomerFIO = model.CustomerFIO;
        }
        public void DelElement(int id)
        {
            Customer element = source.Customer.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Customer.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
