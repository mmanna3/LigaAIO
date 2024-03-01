namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaEquiposDeEliminacionDirecta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipoEliminacionDirecta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TorneoId = c.Int(nullable: false),
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipo", t => t.EquipoId)
                .ForeignKey("dbo.Torneo", t => t.TorneoId)
                .Index(t => new { t.TorneoId, t.EquipoId }, unique: true, name: "IX_TorneoYEquipo");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipoEliminacionDirecta", "TorneoId", "dbo.Torneo");
            DropForeignKey("dbo.EquipoEliminacionDirecta", "EquipoId", "dbo.Equipo");
            DropIndex("dbo.EquipoEliminacionDirecta", "IX_TorneoYEquipo");
            DropTable("dbo.EquipoEliminacionDirecta");
        }
    }
}
