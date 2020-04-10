namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LaDefinitivaDeLosDelegados : DbMigration
    {
	    public override void Up()
	    {
		    Sql("delete from delegado");
			Sql("insert into delegado (Descripcion, Telefono, ClubId)\r\nselect delegado, telefono, id from club");
	    }

		public override void Down()
        {
        }
    }
}
