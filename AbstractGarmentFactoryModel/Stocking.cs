using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryModel
{
    public class Stocking
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string StockingName { get; set; }
    }
}
