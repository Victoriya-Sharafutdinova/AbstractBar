﻿using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Attributies;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы со складами")]
    public interface IStorageService
    {
        [CustomMethod("Метод получения списка складов")]
        List<StorageViewModel> GetList();

        [CustomMethod("Метод получения склада по id")]
        StorageViewModel GetElement(int id);

        [CustomMethod("Метод добавление склада")]
        void AddElement(StorageBindingModel model);

        [CustomMethod("Метод изменения данных по складу")]
        void UpdElement(StorageBindingModel model);

        [CustomMethod("Метод удаления склада")]
        void DelElement(int id);
    }
}
