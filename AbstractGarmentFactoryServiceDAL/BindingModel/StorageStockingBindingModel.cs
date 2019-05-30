using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.BindingModel
{
    public class StorageStockingBindingModel
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int StockingId { get; set; }

        public int Amount { get; set; }
    }
}
