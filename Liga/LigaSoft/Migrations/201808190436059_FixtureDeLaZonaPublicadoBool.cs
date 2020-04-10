namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixtureDeLaZonaPublicadoBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zona", "FixturePublicado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Zona", "FixturePublicado");
        }
    }
}
