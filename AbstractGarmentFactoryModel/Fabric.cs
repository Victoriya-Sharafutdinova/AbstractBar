using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryModel
{
    public class Fabric
    {
        public int Id { get; set; }

        [Required]
        public string FabricName { get; set; }

        [Required]
        public decimal Value { get; set; }

        [ForeignKey("FabricId")]
        public virtual List<Indent> Indents { get; set; }

        [ForeignKey("FabricId")]
        public virtual List<FabricStocking> FabricStockings { get; set; }
    }
}
