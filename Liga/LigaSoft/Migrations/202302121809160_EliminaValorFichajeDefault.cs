namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminaValorFichajeDefault : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ParametroGlobal", "ValorPorDefectoEnPesosDelConceptoFichaje");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParametroGlobal", "ValorPorDefectoEnPesosDelConceptoFichaje", c => c.Int(nullable: false));
        }
    }
}
