namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaValorDelFichajeEnPesosATorneoTipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TorneoTipo", "ValorDelFichajeEnPesos", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TorneoTipo", "ValorDelFichajeEnPesos");
        }
    }
}
