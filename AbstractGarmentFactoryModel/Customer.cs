using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryModel
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("ФИО")]
        public string CustomerFIO { get; set; }

        [ForeignKey("CustomerId")]
        public virtual List<Indent> Indents { get; set; }
    }
}
