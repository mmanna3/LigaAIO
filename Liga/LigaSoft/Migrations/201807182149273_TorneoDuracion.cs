namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TorneoDuracion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TorneoTipo", "Duracion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TorneoTipo", "Duracion");
        }
    }
}
