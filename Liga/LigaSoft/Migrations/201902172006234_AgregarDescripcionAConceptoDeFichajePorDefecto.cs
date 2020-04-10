namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarDescripcionAConceptoDeFichajePorDefecto : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM ParametrizacionGlobal; INSERT INTO ParametrizacionGlobal VALUES ('ValorPorDefectoEnPesosDelConceptoFichaje', '0', 'Valor por defecto en pesos del concepto fichaje') ");
        }
        
        public override void Down()
        {
        }
    }
}
