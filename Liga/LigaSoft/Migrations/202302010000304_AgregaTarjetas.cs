namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaTarjetas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JugadorEquipo", "TarjetasAmarillas", c => c.Int(nullable: false));
            AddColumn("dbo.JugadorEquipo", "TarjetasRojas", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JugadorEquipo", "TarjetasRojas");
            DropColumn("dbo.JugadorEquipo", "TarjetasAmarillas");
        }
    }
}
