namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MesEnLasCuotas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovimientoEntradaCuota",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Mes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovimientoEntrada", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovimientoEntradaCuota", "Id", "dbo.MovimientoEntrada");
            DropIndex("dbo.MovimientoEntradaCuota", new[] { "Id" });
            DropTable("dbo.MovimientoEntradaCuota");
        }
    }
}
