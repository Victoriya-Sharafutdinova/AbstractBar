using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace AbstractGarmentFactoryServiceDAL.BindingModel
{
    [DataContract]
    public class IndentBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public int FabricId { get; set; }

        [DataMember]
        public int? ImplementerId { get; set; }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public decimal Total { get; set; }
    }
}
