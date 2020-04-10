namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FechaPublicada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fecha", "Publicada", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fecha", "Publicada");
        }
    }
}
