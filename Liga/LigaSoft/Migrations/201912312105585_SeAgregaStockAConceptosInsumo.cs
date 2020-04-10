namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgregaStockAConceptosInsumo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConceptoInsumo", "Stock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConceptoInsumo", "Stock");
        }
    }
}
