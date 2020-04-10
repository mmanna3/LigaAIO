namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedPublicidadesDefault : DbMigration
    {
        public override void Up()
        {
	        Sql(@"
					insert into Publicidad VALUES ('Publicite aquí', 'http://www.edefi.com.ar', 1);
					insert into Publicidad VALUES ('Publicite aquí', 'http://www.edefi.com.ar', 2);
					insert into Publicidad VALUES ('Publicite aquí', 'http://www.edefi.com.ar', 3);
					insert into Publicidad VALUES ('Publicite aquí', 'http://www.edefi.com.ar', 4);
				");
		}
        
        public override void Down()
        {
        }
    }
}
