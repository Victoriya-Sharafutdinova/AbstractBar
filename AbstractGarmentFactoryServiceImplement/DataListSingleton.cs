using AbstractGarmentFactoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceImplement
{
    class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Customer> Customer { get; set; }
        public List<Stocking> Stocking { get; set; }

        public List<Indent> Indents { get; set; }

        public List<Fabric> Fabric { get; set; }

        public List<FabricStocking> FabricStocking { get; set; }

        private DataListSingleton()
        {
            Customer = new List<Customer>();
            Stocking = new List<Stocking>();
            Indents = new List<Indent>();
            Fabric = new List<Fabric>();
            FabricStocking = new List<FabricStocking>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null) { instance = new DataListSingleton(); }

            return instance;
        }
    }
}
