namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedTorneoFormato : DbMigration
    {
        public override void Up()
        {
	        Sql("UPDATE TorneoTipo SET Formato = 1");
		}
        
        public override void Down()
        {
        }
    }
}
