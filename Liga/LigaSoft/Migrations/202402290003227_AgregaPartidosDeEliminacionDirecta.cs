namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaPartidosDeEliminacionDirecta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PartidoEliminacionDirecta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TorneoId = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        Fase = c.Int(nullable: false),
                        LocalId = c.Int(nullable: false),
                        VisitanteId = c.Int(nullable: false),
                        GolesLocal = c.String(nullable: false),
                        GolesVisitante = c.String(nullable: false),
                        PenalesLocal = c.Int(),
                        PenalesVisitante = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId)
                .ForeignKey("dbo.Equipo", t => t.LocalId)
                .ForeignKey("dbo.Torneo", t => t.TorneoId)
                .ForeignKey("dbo.Equipo", t => t.VisitanteId)
                .Index(t => new { t.TorneoId, t.CategoriaId, t.Fase }, unique: true, name: "IX_TorneoYCategoriaYFase")
                .Index(t => t.LocalId)
                .Index(t => t.VisitanteId);
            
            AddColumn("dbo.Torneo", "LlaveDeEliminacionDirecta", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartidoEliminacionDirecta", "VisitanteId", "dbo.Equipo");
            DropForeignKey("dbo.PartidoEliminacionDirecta", "TorneoId", "dbo.Torneo");
            DropForeignKey("dbo.PartidoEliminacionDirecta", "LocalId", "dbo.Equipo");
            DropForeignKey("dbo.PartidoEliminacionDirecta", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.PartidoEliminacionDirecta", new[] { "VisitanteId" });
            DropIndex("dbo.PartidoEliminacionDirecta", new[] { "LocalId" });
            DropIndex("dbo.PartidoEliminacionDirecta", "IX_TorneoYCategoriaYFase");
            DropColumn("dbo.Torneo", "LlaveDeEliminacionDirecta");
            DropTable("dbo.PartidoEliminacionDirecta");
        }
    }
}
