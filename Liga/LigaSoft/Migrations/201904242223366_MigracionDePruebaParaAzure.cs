using System.Data.SqlClient;

namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigracionDePruebaParaAzure : DbMigration
    {
        public override void Up()
        {
			Sql("update torneo set publico = 1 where publico = 1");
		}
        
        public override void Down()
        {
        }
    }
}
