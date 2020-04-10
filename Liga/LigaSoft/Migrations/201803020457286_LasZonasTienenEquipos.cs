namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LasZonasTienenEquipos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipo", "ZonaId", c => c.Int());
            CreateIndex("dbo.Equipo", "ZonaId");
            AddForeignKey("dbo.Equipo", "ZonaId", "dbo.Zona", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipo", "ZonaId", "dbo.Zona");
            DropIndex("dbo.Equipo", new[] { "ZonaId" });
            DropColumn("dbo.Equipo", "ZonaId");
        }
    }
}
