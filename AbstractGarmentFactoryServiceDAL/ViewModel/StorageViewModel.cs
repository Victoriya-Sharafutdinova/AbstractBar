using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.ViewModel
{
    public class StorageViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название склада")]
        public string StorageName { get; set; }

        public List<StorageStockingViewModel> StorageStocking { get; set; }
    }
}
