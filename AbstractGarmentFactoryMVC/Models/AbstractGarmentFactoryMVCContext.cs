using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AbstractGarmentFactoryMVC.Models
{
    public class AbstractGarmentFactoryMVCContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AbstractGarmentFactoryMVCContext() : base("name=AbstractGarmentFactoryMVCContext")
        {
        }

        public System.Data.Entity.DbSet<AbstractGarmentFactoryModel.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<AbstractGarmentFactoryModel.Fabric> Fabrics { get; set; }

        public System.Data.Entity.DbSet<AbstractGarmentFactoryModel.Indent> Indents { get; set; }

        public System.Data.Entity.DbSet<AbstractGarmentFactoryModel.Stocking> Stockings { get; set; }

        public System.Data.Entity.DbSet<AbstractGarmentFactoryModel.FabricStocking> FabricStockings { get; set; }
    }
}
