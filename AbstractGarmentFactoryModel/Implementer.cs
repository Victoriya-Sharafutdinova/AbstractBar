using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractGarmentFactoryModel
{
    public class Implementer
    {
        public int Id { get; set; }

        [Required]
        public string ImplementerFIO { get; set; }

        [ForeignKey("ImplementerId")]
        public virtual List<Indent> Indents { get; set; }
    }
}
