namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PruebaASPHostPortal2 : DbMigration
    {
        public override void Up()
        {
	        Sql(@"
					CREATE TABLE Persons (
										PersonID int
											);

					DROP TABLE Persons");
		}
        
        public override void Down()
        {
        }
    }
}
