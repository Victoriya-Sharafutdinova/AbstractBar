﻿using System;
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
        public IndentStatus Condition { get; set; }

        //[DataType(DataType.Date)]
       // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
