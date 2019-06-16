using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace AbstractGarmentFactoryServiceDAL.BindingModel
{
    [DataContract]
    public class StockingBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string StockingName { get; set; }
    }
}
