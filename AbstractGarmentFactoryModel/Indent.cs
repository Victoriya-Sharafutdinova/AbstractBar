using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Количество")]
        public int Amount { get; set; }

        [DisplayName("Стоимость")]
        public decimal Total { get; set; }

        [DisplayName("Состояние")]
        public IndentCondition Condition { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
