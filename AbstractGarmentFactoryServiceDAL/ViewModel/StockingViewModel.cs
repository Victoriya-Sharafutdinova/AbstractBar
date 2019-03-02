using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.ViewModel
{
    public class StockingViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название заготовки")]
        public string StockingName { get; set; }
    }
}
