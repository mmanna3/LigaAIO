namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminaEquiposEliminacionDirectaPorqueSePuedenSeleccionarTodosYGolesNoEsMasRequerido : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EquipoEliminacionDirecta", "EquipoId", "dbo.Equipo");
            DropForeignKey("dbo.EquipoEliminacionDirecta", "TorneoId", "dbo.Torneo");
            DropIndex("dbo.EquipoEliminacionDirecta", "IX_TorneoYEquipo");
            AlterColumn("dbo.PartidoEliminacionDirecta", "GolesLocal", c => c.String());
            AlterColumn("dbo.PartidoEliminacionDirecta", "GolesVisitante", c => c.String());
            DropTable("dbo.EquipoEliminacionDirecta");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EquipoEliminacionDirecta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TorneoId = c.Int(nullable: false),
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.PartidoEliminacionDirecta", "GolesVisitante", c => c.String(nullable: false));
            AlterColumn("dbo.PartidoEliminacionDirecta", "GolesLocal", c => c.String(nullable: false));
            CreateIndex("dbo.EquipoEliminacionDirecta", new[] { "TorneoId", "EquipoId" }, unique: true, name: "IX_TorneoYEquipo");
            AddForeignKey("dbo.EquipoEliminacionDirecta", "TorneoId", "dbo.Torneo", "Id");
            AddForeignKey("dbo.EquipoEliminacionDirecta", "EquipoId", "dbo.Equipo", "Id");
        }
    }
}
