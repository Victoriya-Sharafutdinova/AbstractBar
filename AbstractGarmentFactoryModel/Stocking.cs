using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryModel
{
    public class Stocking
    {
        public int Id { get; set; }

        [Required]
        public string StockingName { get; set; }

        [ForeignKey("StockingId")]
        public virtual List<StorageStocking> StorageStockings { get; set; }

        [ForeignKey("StockingId")]
        public virtual List<FabricStocking> FabricStockings { get; set; }
    }
}
