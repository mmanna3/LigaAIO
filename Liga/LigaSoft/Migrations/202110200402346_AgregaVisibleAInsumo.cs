namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaVisibleAInsumo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConceptoInsumo", "Visible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConceptoInsumo", "Visible");
        }
    }
}
