namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgregaEntidadGoleador : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Goleador",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartidoId = c.Int(nullable: false),
                        JugadorId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jugador", t => t.JugadorId)
                .ForeignKey("dbo.Partido", t => t.PartidoId)
                .Index(t => t.PartidoId)
                .Index(t => t.JugadorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Goleador", "PartidoId", "dbo.Partido");
            DropForeignKey("dbo.Goleador", "JugadorId", "dbo.Jugador");
            DropIndex("dbo.Goleador", new[] { "JugadorId" });
            DropIndex("dbo.Goleador", new[] { "PartidoId" });
            DropTable("dbo.Goleador");
        }
    }
}
