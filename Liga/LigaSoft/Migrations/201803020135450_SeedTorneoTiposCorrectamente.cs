namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedTorneoTiposCorrectamente : DbMigration
    {
        public override void Up()
        {
			Sql(@"
					DELETE FROM TorneoTipo

					INSERT INTO [dbo].[TorneoTipo] ([Descripcion], [ValidezDelCarnetEnAnios], [LoQueSeImprimeEnElCarnet]) VALUES (N'Matutino 5 categorías', 2, N'Matutino')
					INSERT INTO [dbo].[TorneoTipo] ([Descripcion], [ValidezDelCarnetEnAnios], [LoQueSeImprimeEnElCarnet]) VALUES (N'Matutino 6 categorías', 2, N'Matutino')
					INSERT INTO [dbo].[TorneoTipo] ([Descripcion], [ValidezDelCarnetEnAnios], [LoQueSeImprimeEnElCarnet]) VALUES (N'Vespertino', 2, N'Vespertino')
					INSERT INTO [dbo].[TorneoTipo] ([Descripcion], [ValidezDelCarnetEnAnios], [LoQueSeImprimeEnElCarnet]) VALUES (N'Femenino', 2, N'Femenino')
					INSERT INTO [dbo].[TorneoTipo] ([Descripcion], [ValidezDelCarnetEnAnios], [LoQueSeImprimeEnElCarnet]) VALUES (N'Futsal', 1, N'Futsal')
				");
        }
        
        public override void Down()
        {
        }
    }
}
