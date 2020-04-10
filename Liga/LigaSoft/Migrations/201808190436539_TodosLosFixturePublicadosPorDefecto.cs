namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodosLosFixturePublicadosPorDefecto : DbMigration
    {
        public override void Up()
        {
			Sql(@"	UPDATE zona
					SET fixturepublicado = 1");
        }
        
        public override void Down()
        {
        }
    }
}
