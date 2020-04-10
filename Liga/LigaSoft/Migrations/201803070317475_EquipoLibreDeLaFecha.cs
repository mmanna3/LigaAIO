namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EquipoLibreDeLaFecha : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fecha", "EquipoLibreId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fecha", "EquipoLibreId");
        }
    }
}
