namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoQueSeImprimeEnElCarnet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TorneoTipo", "LoQueSeImprimeEnElCarnet", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TorneoTipo", "LoQueSeImprimeEnElCarnet");
        }
    }
}
