namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaFormaDePagoEnTodosLosMovimientos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovimientoEntradaConClub", "FormaDePago", c => c.Int(nullable: false));
            AddColumn("dbo.MovimientoEntradaSinClub", "FormaDePago", c => c.Int(nullable: false));
            AddColumn("dbo.MovimientoSalida", "FormaDePago", c => c.Int(nullable: false));
            Sql("update MovimientoEntradaConClub set formadepago = 1");
            Sql("update MovimientoEntradaSinClub set formadepago = 1");
            Sql("update MovimientoSalida set formadepago = 1");
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovimientoSalida", "FormaDePago");
            DropColumn("dbo.MovimientoEntradaSinClub", "FormaDePago");
            DropColumn("dbo.MovimientoEntradaConClub", "FormaDePago");
        }
    }
}
