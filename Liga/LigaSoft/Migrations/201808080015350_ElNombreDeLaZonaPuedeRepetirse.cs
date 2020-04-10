namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ElNombreDeLaZonaPuedeRepetirse : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Zona", "IX_NombreYTorneo");
            CreateIndex("dbo.Zona", "TorneoId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Zona", new[] { "TorneoId" });
            CreateIndex("dbo.Zona", new[] { "Nombre", "TorneoId" }, unique: true, name: "IX_NombreYTorneo");
        }
    }
}
