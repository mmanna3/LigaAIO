namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaLlaveEliminacionDirectaPublicadaEnTorneo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Torneo", "LlaveEliminacionDirectaPublicada", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Torneo", "LlaveEliminacionDirectaPublicada");
        }
    }
}
