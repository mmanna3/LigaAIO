namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedTodosLosPartidosVerificadosEnTrue : DbMigration
    {
        public override void Up()
        {
			Sql("update jornada\r\nset ResultadosVerificados = 1");
        }
        
        public override void Down()
        {
        }
    }
}
