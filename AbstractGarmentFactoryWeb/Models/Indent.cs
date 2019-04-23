using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbstractGarmentFactoryWeb.Models
{
    public class Indent
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int FabricId { get; set; }

        public int Amount { get; set; }

        public decimal Total { get; set; }

        public IndentStatus Condition { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }
    }
}