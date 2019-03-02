using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.BindingModel
{
    public  class FabricBindingModel
    {
        public int Id { get; set; }

        public string FabricName { get; set; }

        public decimal Value { get; set; }

        public List<FabricStockingBindingModel> FabricStocking { get; set; }
    }
}
