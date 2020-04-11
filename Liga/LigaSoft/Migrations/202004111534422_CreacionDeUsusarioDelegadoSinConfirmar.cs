namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionDeUsusarioDelegadoSinConfirmar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsusarioDelegadoSinConfirmar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 100, unicode: false),
                        Password = c.String(maxLength: 100, unicode: false),
                        ClubId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Club", t => t.ClubId)
                .Index(t => t.ClubId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsusarioDelegadoSinConfirmar", "ClubId", "dbo.Club");
            DropIndex("dbo.UsusarioDelegadoSinConfirmar", new[] { "ClubId" });
            DropTable("dbo.UsusarioDelegadoSinConfirmar");
        }
    }
}
