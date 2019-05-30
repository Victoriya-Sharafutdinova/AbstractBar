namespace AbstractGarmentFactoryServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Indents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        FabricId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Condition = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Fabrics", t => t.FabricId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.FabricId);
            
            CreateTable(
                "dbo.Fabrics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FabricName = c.String(),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FabricStockings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FabricId = c.Int(nullable: false),
                        StockingId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stockings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockingName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StorageStockings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageId = c.Int(nullable: false),
                        StockingId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Indents", "FabricId", "dbo.Fabrics");
            DropForeignKey("dbo.Indents", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Indents", new[] { "FabricId" });
            DropIndex("dbo.Indents", new[] { "CustomerId" });
            DropTable("dbo.StorageStockings");
            DropTable("dbo.Storages");
            DropTable("dbo.Stockings");
            DropTable("dbo.FabricStockings");
            DropTable("dbo.Fabrics");
            DropTable("dbo.Indents");
            DropTable("dbo.Customers");
        }
    }
}
