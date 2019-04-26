using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryModel
{
    public class Customer
    {
        public int Id { get; set; }

        [DisplayName("ФИО")]
        public string CustomerFIO { get; set; }
    }
}
