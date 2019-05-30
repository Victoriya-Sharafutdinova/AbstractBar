using AbstractGarmentFactoryModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGarmentFactoryServiceImplementDataBase
{
    public class AbstractDBScope : DbContext
    {
        public AbstractDBScope() : base("AbstractDatabase")
        {
            //настройки конфигурации для entity            
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        } 

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Stocking> Stockings { get; set; }

        public virtual DbSet<Indent> Indents { get; set; }

        public virtual DbSet<Fabric> Fabrics { get; set; }

        public virtual DbSet<FabricStocking> FabricStockings { get; set; }

        public virtual DbSet<Storage> Storages { get; set; }

        public virtual DbSet<StorageStocking> StorageStockings { get; set; }

        public virtual DbSet<Implementer> Implementer { get; set; }

    }
}
