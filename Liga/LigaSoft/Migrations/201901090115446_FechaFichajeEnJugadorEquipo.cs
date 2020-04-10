namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FechaFichajeEnJugadorEquipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JugadorEquipo", "FechaFichaje", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JugadorEquipo", "FechaFichaje");
        }
    }
}
