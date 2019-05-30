namespace AbstractGarmentFactoryServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Implementers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImplementerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Indents", "ImplementerId", c => c.Int());
            CreateIndex("dbo.Indents", "ImplementerId");
            AddForeignKey("dbo.Indents", "ImplementerId", "dbo.Implementers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Indents", "ImplementerId", "dbo.Implementers");
            DropIndex("dbo.Indents", new[] { "ImplementerId" });
            DropColumn("dbo.Indents", "ImplementerId");
            DropTable("dbo.Implementers");
        }
    }
}
