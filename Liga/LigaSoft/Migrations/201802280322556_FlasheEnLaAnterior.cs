namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlasheEnLaAnterior : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categoria", "ZonaId", "dbo.Zona");
            DropIndex("dbo.Categoria", "IX_NombreYZona");
            AddColumn("dbo.Categoria", "TorneoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Categoria", new[] { "Nombre", "TorneoId" }, unique: true, name: "IX_NombreYTorneo");
            AddForeignKey("dbo.Categoria", "TorneoId", "dbo.Torneo", "Id", cascadeDelete: true);
            DropColumn("dbo.Categoria", "ZonaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categoria", "ZonaId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Categoria", "TorneoId", "dbo.Torneo");
            DropIndex("dbo.Categoria", "IX_NombreYTorneo");
            DropColumn("dbo.Categoria", "TorneoId");
            CreateIndex("dbo.Categoria", new[] { "Nombre", "ZonaId" }, unique: true, name: "IX_NombreYZona");
            AddForeignKey("dbo.Categoria", "ZonaId", "dbo.Zona", "Id", cascadeDelete: true);
        }
    }
}
