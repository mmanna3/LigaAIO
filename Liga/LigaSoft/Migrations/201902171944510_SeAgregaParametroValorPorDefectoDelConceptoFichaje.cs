namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgregaParametroValorPorDefectoDelConceptoFichaje : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ParametrizacionGlobal VALUES ('ValorPorDefectoEnPesosDelConceptoFichaje', '0') ");
        }
        
        public override void Down()
        {
        }
    }
}
