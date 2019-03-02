using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.ViewModel
{
    public class FabricStockingViewModel
    {
        public int Id { get; set; }

        public int FabricId { get; set; }

        public int StockingId { get; set; }

        [DisplayName("Количество")]
        public int Amount { get; set; }

        [DisplayName("Заготовки")]
        public string StockingName { get; set; }
    }
}
