namespace AbstractGarmentFactoryMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stockings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockingName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stockings");
            DropTable("dbo.Indents");
            DropTable("dbo.FabricStockings");
            DropTable("dbo.Fabrics");
        }
    }
}
