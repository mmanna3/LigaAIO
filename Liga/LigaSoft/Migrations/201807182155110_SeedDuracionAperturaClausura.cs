namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedDuracionAperturaClausura : DbMigration
    {
        public override void Up()
        {
			Sql("UPDATE TorneoTipo SET Duracion = 1");
        }
        
        public override void Down()
        {
        }
    }
}
