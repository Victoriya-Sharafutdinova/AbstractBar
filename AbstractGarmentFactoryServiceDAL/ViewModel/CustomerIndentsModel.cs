using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.ViewModel
{
     public class CustomerIndentsModel
    {
        public string CustomerName { get; set; }

        public string DateCreate { get; set; }

        public string FabricName { get; set; }

        public int Amount { get; set; }

        public decimal Total { get; set; }

        public string Condition { get; set; }
    }
}
