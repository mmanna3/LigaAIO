namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedConceptos : DbMigration
    {
        public override void Up()
        {
	        Sql(@"
					DELETE FROM conceptolibre
					DELETE FROM conceptocuota
					DELETE FROM conceptofichaje

					INSERT INTO conceptolibre
					VALUES (1)

					INSERT INTO conceptocuota
					VALUES (2)

					INSERT INTO conceptofichaje
					VALUES (3)
				");
        }
        
        public override void Down()
        {
        }
    }
}
