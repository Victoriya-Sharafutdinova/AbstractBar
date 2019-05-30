namespace AbstractGarmentFactoryServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fabrics", "FabricName", c => c.String(nullable: false));
            AlterColumn("dbo.Stockings", "StockingName", c => c.String(nullable: false));
            AlterColumn("dbo.Storages", "StorageName", c => c.String(nullable: false));
            CreateIndex("dbo.FabricStockings", "FabricId");
            CreateIndex("dbo.FabricStockings", "StockingId");
            CreateIndex("dbo.StorageStockings", "StorageId");
            CreateIndex("dbo.StorageStockings", "StockingId");
            AddForeignKey("dbo.FabricStockings", "FabricId", "dbo.Fabrics", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FabricStockings", "StockingId", "dbo.Stockings", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StorageStockings", "StockingId", "dbo.Stockings", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StorageStockings", "StorageId", "dbo.Storages", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StorageStockings", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.StorageStockings", "StockingId", "dbo.Stockings");
            DropForeignKey("dbo.FabricStockings", "StockingId", "dbo.Stockings");
            DropForeignKey("dbo.FabricStockings", "FabricId", "dbo.Fabrics");
            DropIndex("dbo.StorageStockings", new[] { "StockingId" });
            DropIndex("dbo.StorageStockings", new[] { "StorageId" });
            DropIndex("dbo.FabricStockings", new[] { "StockingId" });
            DropIndex("dbo.FabricStockings", new[] { "FabricId" });
            AlterColumn("dbo.Storages", "StorageName", c => c.String());
            AlterColumn("dbo.Stockings", "StockingName", c => c.String());
            AlterColumn("dbo.Fabrics", "FabricName", c => c.String());
        }
    }
}
