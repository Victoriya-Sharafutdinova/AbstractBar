namespace AbstractGarmentFactoryServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Indents", "ImplementerId", "dbo.Implementers");
            DropForeignKey("dbo.MessageInfoes", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Indents", new[] { "ImplementerId" });
            DropIndex("dbo.MessageInfoes", new[] { "CustomerId" });
            DropColumn("dbo.Customers", "Mail");
            DropColumn("dbo.Indents", "ImplementerId");
            DropTable("dbo.Implementers");
            DropTable("dbo.MessageInfoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MessageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Implementers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImplementerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Indents", "ImplementerId", c => c.Int());
            AddColumn("dbo.Customers", "Mail", c => c.String());
            CreateIndex("dbo.MessageInfoes", "CustomerId");
            CreateIndex("dbo.Indents", "ImplementerId");
            AddForeignKey("dbo.MessageInfoes", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Indents", "ImplementerId", "dbo.Implementers", "Id");
        }
    }
}
