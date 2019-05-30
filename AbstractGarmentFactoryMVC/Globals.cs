using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceImplement.Implementations;
using AbstractGarmentFactoryServiceImplementDataBase;
using AbstractGarmentFactoryServiceImplementDataBase.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbstractGarmentFactoryMVC
{
    public static class Globals
    {
        public static AbstractDBScope DbContext { get; } = new AbstractDBScope();

        public static ICustomerService CustomerService { get; } = new CustomerServiceDB(DbContext);

        public static IStockingService StockingService { get; } = new StockingServiceDB(DbContext);

        public static IFabricService FabricService { get; } = new FabricServiceDB(DbContext);

        public static IMainService MainService { get; } = new MainServiceDB(DbContext);

        public static IStorageService StorageService { get; } = new StorageServiceDB(DbContext);

    }
}