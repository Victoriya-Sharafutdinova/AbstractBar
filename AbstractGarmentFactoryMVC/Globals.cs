using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceImplement.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbstractGarmentFactoryMVC
{
    public static class Globals

    {

        public static ICustomerService CustomerService { get; } = new CustomerServiceList();

        public static IStockingService StockingService { get; } = new StockingServiceList();

        public static IFabricService FabricService { get; } = new FabricServiceList();

        public static IMainService MainService { get; } = new MainServiceList();

        public static IStorageService StorageService { get; } = new StorageServiceList();

    }
}