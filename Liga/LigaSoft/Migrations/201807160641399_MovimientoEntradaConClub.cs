namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimientoEntradaConClub : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MovimientoEntrada", newName: "MovimientoEntradaConClub");
            RenameTable(name: "dbo.MovimientoEntradaCuota", newName: "MovimientoEntradaConClubCuota");
            RenameColumn(table: "dbo.Pago", name: "MovimientoEntradaId", newName: "MovimientoEntradaConClubId");
            RenameIndex(table: "dbo.Pago", name: "IX_MovimientoEntradaId", newName: "IX_MovimientoEntradaConClubId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Pago", name: "IX_MovimientoEntradaConClubId", newName: "IX_MovimientoEntradaId");
            RenameColumn(table: "dbo.Pago", name: "MovimientoEntradaConClubId", newName: "MovimientoEntradaId");
            RenameTable(name: "dbo.MovimientoEntradaConClubCuota", newName: "MovimientoEntradaCuota");
            RenameTable(name: "dbo.MovimientoEntradaConClub", newName: "MovimientoEntrada");
        }
    }
}
