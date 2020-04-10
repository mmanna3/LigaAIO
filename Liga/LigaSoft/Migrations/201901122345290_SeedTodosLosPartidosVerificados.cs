namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedTodosLosPartidosVerificados : DbMigration
    {
        public override void Up()
        {
			Sql("update partido\r\nset ResultadoVerificado = 1");
        }
        
        public override void Down()
        {
        }
    }
}
