namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delegados : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Delegado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Telefono = c.String(nullable: false),
                        ClubId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Club", t => t.ClubId)
                .Index(t => t.ClubId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Delegado", "ClubId", "dbo.Club");
            DropIndex("dbo.Delegado", new[] { "ClubId" });
            DropTable("dbo.Delegado");
        }
    }
}
