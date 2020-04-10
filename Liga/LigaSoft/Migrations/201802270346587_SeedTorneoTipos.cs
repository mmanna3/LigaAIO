namespace LigaSoft.Migrations
{
	using System.Data.Entity.Migrations;
    
    public partial class SeedTorneoTipos : DbMigration
    {
	    public override void Up()
	    {
		    Sql("INSERT INTO [dbo].[TorneoTipo] ([Descripcion], [ValidezDelCarnetEnAnios]) VALUES (N\'Matutino\', 2) " +
				"INSERT INTO [dbo].[TorneoTipo] ([Descripcion], [ValidezDelCarnetEnAnios]) VALUES (N\'Vespertino\', 2)" +
				"INSERT INTO [dbo].[TorneoTipo] ([Descripcion], [ValidezDelCarnetEnAnios]) VALUES (N\'Torneo de verano\', 2)");
	    }

		public override void Down()
        {
        }
    }
}
