namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CadaEquipoPuedeJugarVariosTorneosRelampago : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZonaRelampagoEquipo",
                c => new
                    {
                        ZonaId = c.Int(nullable: false),
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ZonaId, t.EquipoId })
                .ForeignKey("dbo.Equipo", t => t.EquipoId)
                .ForeignKey("dbo.Zona", t => t.ZonaId)
                .Index(t => t.ZonaId)
                .Index(t => t.EquipoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZonaRelampagoEquipo", "ZonaId", "dbo.Zona");
            DropForeignKey("dbo.ZonaRelampagoEquipo", "EquipoId", "dbo.Equipo");
            DropIndex("dbo.ZonaRelampagoEquipo", new[] { "EquipoId" });
            DropIndex("dbo.ZonaRelampagoEquipo", new[] { "ZonaId" });
            DropTable("dbo.ZonaRelampagoEquipo");
        }
    }
}
