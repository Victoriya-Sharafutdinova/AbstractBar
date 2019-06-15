using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace AbstractGarmentFactoryServiceDAL.BindingModel
{
    [DataContract]
    public class StorageStockingBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StorageId { get; set; }

        [DataMember]
        public int StockingId { get; set; }

        [DataMember]
        public int Amount { get; set; }
    }
}
