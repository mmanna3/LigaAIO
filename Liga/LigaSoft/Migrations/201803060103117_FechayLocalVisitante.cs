namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FechayLocalVisitante : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocalVisitante",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaId = c.Int(nullable: false),
                        LocalId = c.Int(nullable: false),
                        VisitanteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fecha", t => t.FechaId, cascadeDelete: true)
                .ForeignKey("dbo.Equipo", t => t.LocalId)
                .ForeignKey("dbo.Equipo", t => t.VisitanteId)
                .Index(t => t.FechaId)
                .Index(t => t.LocalId)
                .Index(t => t.VisitanteId);
            
            CreateTable(
                "dbo.Fecha",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.Int(nullable: false),
                        DiaDeLaFecha = c.DateTime(nullable: false),
                        ZonaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Zona", t => t.ZonaId, cascadeDelete: true)
                .Index(t => t.ZonaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocalVisitante", "VisitanteId", "dbo.Equipo");
            DropForeignKey("dbo.LocalVisitante", "LocalId", "dbo.Equipo");
            DropForeignKey("dbo.Fecha", "ZonaId", "dbo.Zona");
            DropForeignKey("dbo.LocalVisitante", "FechaId", "dbo.Fecha");
            DropIndex("dbo.Fecha", new[] { "ZonaId" });
            DropIndex("dbo.LocalVisitante", new[] { "VisitanteId" });
            DropIndex("dbo.LocalVisitante", new[] { "LocalId" });
            DropIndex("dbo.LocalVisitante", new[] { "FechaId" });
            DropTable("dbo.Fecha");
            DropTable("dbo.LocalVisitante");
        }
    }
}
