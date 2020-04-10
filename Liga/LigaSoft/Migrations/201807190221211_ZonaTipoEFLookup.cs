namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZonaTipoEFLookup : DbMigration
    {
        public override void Up()
        {
			Sql("UPDATE Zona SET Tipo = 1");
        }
        
        public override void Down()
        {
        }
    }
}
