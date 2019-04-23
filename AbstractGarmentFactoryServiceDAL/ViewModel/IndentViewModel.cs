using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AbstractGarmentFactoryServiceDAL.ViewModel
{
    [DataContract]
    public class IndentViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        [DisplayName("ФИО Клиента")]
        public string CustomerFIO { get; set; }

        [DataMember]
        public int FabricId { get; set; }

        [DataMember]
        [DisplayName("Изделие")]
        public string FabricName { get; set; }

        [DataMember]
        public int? ImplementerId { get; set; }

        [DataMember]
        public string ImplementerName { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public int Amount { get; set; }

        [DataMember]
        [DisplayName("Сумма")]
        public decimal Total { get; set; }

        [DataMember]
        [DisplayName("Статус")]
        public string Condition { get; set; }

        [DataMember]
        [DisplayName("Дата создания")]
        public string DateCreate { get; set; }

        [DataMember]
        [DisplayName("Дата выполнения")]
        public string DateImplement { get; set; }
    }
}
