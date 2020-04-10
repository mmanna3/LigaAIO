namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedFechaFichajePrimeroEnero2018 : DbMigration
    {
        public override void Up()
        {
			Sql("UPDATE JugadorEquipo SET FechaFichaje = \'2018-01-01 00:00:00.000\'");
        }
        
        public override void Down()
        {
        }
    }
}
