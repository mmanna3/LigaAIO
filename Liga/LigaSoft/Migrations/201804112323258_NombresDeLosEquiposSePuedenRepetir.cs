namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NombresDeLosEquiposSePuedenRepetir : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Equipo", new[] { "Nombre" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Equipo", "Nombre", unique: true);
        }
    }
}
