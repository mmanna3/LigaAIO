namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedConceptosCuotaFichajeLibre : DbMigration
    {
        public override void Up()
        {
			Sql(@"
					SET IDENTITY_INSERT Concepto ON

					INSERT INTO Concepto (Id, Descripcion)
					VALUES(1, 'Libre'), (2, 'Cuota'), (3, 'Fichaje')

					INSERT INTO ConceptoLibre
					VALUES(1)

					INSERT INTO ConceptoCuota
					VALUES(2)

					INSERT INTO ConceptoFichaje
					VALUES (3)

					SET IDENTITY_INSERT Concepto OFF
				");
        }
        
        public override void Down()
        {
        }
    }
}
