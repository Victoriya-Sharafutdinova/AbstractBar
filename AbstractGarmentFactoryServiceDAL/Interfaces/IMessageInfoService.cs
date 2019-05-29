using AbstractGarmentFactoryServiceDAL.Attributies;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с почтой")]
    public interface IMessageInfoService
    {
        [CustomMethod("Метод получения списка сообщений")]
        List<MessageInfoViewModel> GetList();

        [CustomMethod("Метод получения сообщения по id")]
        MessageInfoViewModel GetElement(int id);

        [CustomMethod("Метод добавления сообщения")]
        void AddElement(MessageInfoBindingModel model);
    }
}
