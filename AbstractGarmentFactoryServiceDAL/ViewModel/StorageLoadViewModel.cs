using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceDAL.ViewModel
{
    public class StorageLoadViewModel
    {
        public string StorageName { get; set; }

        public int TotalAmount { get; set; }

        public IEnumerable<Tuple<string, int>> Stockings { get; set; }
    }
}
