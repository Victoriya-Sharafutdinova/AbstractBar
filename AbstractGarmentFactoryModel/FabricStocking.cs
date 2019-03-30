using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryModel
{
    public class FabricStocking
    {
        public int Id { get; set; }

        public int FabricId { get; set; }

        public int StockingId { get; set; }

        public int Amount { get; set; }

        public virtual Fabric Fabrics { get; set; }

        public virtual Stocking Stocking { get; set; }


    }
}
