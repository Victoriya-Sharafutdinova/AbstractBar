using AbstractGarmentFactoryModel;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceImplementDataBase.Implementations
{
    public class ImplementerServiceDB
    {
        private AbstractDBScope context;

        public ImplementerServiceDB(AbstractDBScope context)
        {
            this.context = context;
        }

        public List<ImplementerViewModel> GetList()
        {
            List<ImplementerViewModel> result = context.Implementer.Select(rec => new ImplementerViewModel
            {
                Id = rec.Id,
                ImplementerFIO = rec.ImplementerFIO
            }).ToList();
            return result; }

        public ImplementerViewModel GetElement(int id)
        {
            Implementer element = context.Implementer.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ImplementerViewModel
                {
                    Id = element.Id,
                    ImplementerFIO = element.ImplementerFIO
                };
            }
            throw new Exception("Элемент не найден"); }


        public void AddElement(ImplementerBindingModel model)
        {
            Implementer element = context.Implementer.FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            context.Implementer.Add(new Implementer
            {
                ImplementerFIO = model.ImplementerFIO
            });
            context.SaveChanges();
        }

        public void UpdElement(ImplementerBindingModel model)
        {
            Implementer element = context.Implementer.FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = context.Implementer.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ImplementerFIO = model.ImplementerFIO;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Implementer element = context.Implementer.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Implementer.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ImplementerViewModel GetFreeWorker()
        {
            var indentsWorker = context.Implementer.Select(x => new
            {
                ImplId = x.Id,
                Count = context.Indents.Where(o => o.Condition == IndentCondition.Выполняется && o.ImplementerId == x.Id).Count()
            }).OrderBy(x => x.Count).FirstOrDefault();
            if (indentsWorker != null)
            {
                return GetElement(indentsWorker.ImplId);
            }
            return null;
        }
    }
}
