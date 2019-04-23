using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbstractGarmentFactoryWeb.Models
{
    public class FabricStocking
    {
        public int Id { get; set; }

        public int FabricId { get; set; }

        public int StockingId { get; set; }

        public int Amount { get; set; }
    }
}