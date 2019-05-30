using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.ViewModel
{
    public class StorageStockingViewModel
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int StockingId { get; set; }

        [DisplayName("Название компонента")]
        public string StockingName { get; set; }

        [DisplayName("Количество")]
        public int Amount { get; set; }
    }
}
