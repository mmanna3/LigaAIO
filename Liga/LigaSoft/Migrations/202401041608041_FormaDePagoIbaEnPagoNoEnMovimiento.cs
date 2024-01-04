﻿namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormaDePagoIbaEnPagoNoEnMovimiento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pago", "FormaDePago", c => c.Int(nullable: false));
            DropColumn("dbo.MovimientoEntradaConClub", "FormaDePago");
            DropColumn("dbo.MovimientoEntradaSinClub", "FormaDePago");
            DropColumn("dbo.MovimientoSalida", "FormaDePago");
            Sql("update Pago set formadepago = 1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MovimientoSalida", "FormaDePago", c => c.Int(nullable: false));
            AddColumn("dbo.MovimientoEntradaSinClub", "FormaDePago", c => c.Int(nullable: false));
            AddColumn("dbo.MovimientoEntradaConClub", "FormaDePago", c => c.Int(nullable: false));
            DropColumn("dbo.Pago", "FormaDePago");
        }
    }
}
