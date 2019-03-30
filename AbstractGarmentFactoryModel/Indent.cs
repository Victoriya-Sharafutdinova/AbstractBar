using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryModel
{
    public class Indent
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int FabricId { get; set; }

        public int Amount { get; set; }

        public decimal Total { get; set; }

        public IndentCondition Condition { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Fabric Fabric { get; set; }
    }
}
