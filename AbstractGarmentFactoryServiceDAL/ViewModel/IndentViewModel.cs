using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.ViewModel
{
    public class IndentViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [DisplayName("ФИО Клиента")]
        public string CustomerFIO { get; set; }

        public int FabricId { get; set; }

        [DisplayName("Изделие")]
        public string FabricName { get; set; }

        [DisplayName("Количество")]
        public int Amount { get; set; }

        [DisplayName("Сумма")]
        public decimal Total { get; set; }

        [DisplayName("Статус")]
        public string Condition { get; set; }


        [DisplayName("Дата создания")]
        public string DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public string DateImplement { get; set; }
    }
}
