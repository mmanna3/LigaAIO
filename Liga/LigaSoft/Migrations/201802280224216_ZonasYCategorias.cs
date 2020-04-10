namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZonasYCategorias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Zona",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 18),
                        TorneoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Torneo", t => t.TorneoId, cascadeDelete: true)
                .Index(t => new { t.Nombre, t.TorneoId }, unique: true, name: "IX_NombreYTorneo");
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 18),
                        ZonaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Zona", t => t.ZonaId, cascadeDelete: true)
                .Index(t => new { t.Nombre, t.ZonaId }, unique: true, name: "IX_NombreYZona");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zona", "TorneoId", "dbo.Torneo");
            DropForeignKey("dbo.Categoria", "ZonaId", "dbo.Zona");
            DropIndex("dbo.Categoria", "IX_NombreYZona");
            DropIndex("dbo.Zona", "IX_NombreYTorneo");
            DropTable("dbo.Categoria");
            DropTable("dbo.Zona");
        }
    }
}
