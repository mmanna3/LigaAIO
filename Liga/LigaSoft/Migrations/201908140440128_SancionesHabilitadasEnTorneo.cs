namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SancionesHabilitadasEnTorneo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Torneo", "SancionesHabilitadas", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Torneo", "SancionesHabilitadas");
        }
    }
}
