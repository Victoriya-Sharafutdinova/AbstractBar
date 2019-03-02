using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.ViewModel
{
    public class FabricViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название изделия")]
        public string FabricName { get; set; }

        [DisplayName("Цена")]
        public decimal Value { get; set; }
        public List<FabricStockingViewModel> FabricStocking { get; set; }
    }
}
