using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace AbstractGarmentFactoryServiceDAL.BindingModel
{
    [DataContract]
    public  class FabricBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FabricName { get; set; }

        [DataMember]
        public decimal Value { get; set; }

        public List<FabricStockingBindingModel> FabricStocking { get; set; }
    }
}
