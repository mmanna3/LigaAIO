namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Finanzas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movimiento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Total = c.Int(nullable: false),
                        Comentario = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Concepto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pago",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        MovimientoEntradaId = c.Int(nullable: false),
                        Importe = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovimientoEntrada", t => t.MovimientoEntradaId)
                .Index(t => t.MovimientoEntradaId);
            
            CreateTable(
                "dbo.ConceptoCuota",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Concepto", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ConceptoFichaje",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Concepto", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ConceptoInsumo",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Precio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Concepto", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ConceptoLibre",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Concepto", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.MovimientoEntrada",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ConceptoId = c.Int(nullable: false),
                        PrecioUnitario = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        ClubId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movimiento", t => t.Id)
                .ForeignKey("dbo.Concepto", t => t.ConceptoId)
                .ForeignKey("dbo.Club", t => t.ClubId)
                .Index(t => t.Id)
                .Index(t => t.ConceptoId)
                .Index(t => t.ClubId);
            
            CreateTable(
                "dbo.MovimientoSalida",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Proveedor = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movimiento", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovimientoSalida", "Id", "dbo.Movimiento");
            DropForeignKey("dbo.MovimientoEntrada", "ClubId", "dbo.Club");
            DropForeignKey("dbo.MovimientoEntrada", "ConceptoId", "dbo.Concepto");
            DropForeignKey("dbo.MovimientoEntrada", "Id", "dbo.Movimiento");
            DropForeignKey("dbo.ConceptoLibre", "Id", "dbo.Concepto");
            DropForeignKey("dbo.ConceptoInsumo", "Id", "dbo.Concepto");
            DropForeignKey("dbo.ConceptoFichaje", "Id", "dbo.Concepto");
            DropForeignKey("dbo.ConceptoCuota", "Id", "dbo.Concepto");
            DropForeignKey("dbo.Pago", "MovimientoEntradaId", "dbo.MovimientoEntrada");
            DropIndex("dbo.MovimientoSalida", new[] { "Id" });
            DropIndex("dbo.MovimientoEntrada", new[] { "ClubId" });
            DropIndex("dbo.MovimientoEntrada", new[] { "ConceptoId" });
            DropIndex("dbo.MovimientoEntrada", new[] { "Id" });
            DropIndex("dbo.ConceptoLibre", new[] { "Id" });
            DropIndex("dbo.ConceptoInsumo", new[] { "Id" });
            DropIndex("dbo.ConceptoFichaje", new[] { "Id" });
            DropIndex("dbo.ConceptoCuota", new[] { "Id" });
            DropIndex("dbo.Pago", new[] { "MovimientoEntradaId" });
            DropTable("dbo.MovimientoSalida");
            DropTable("dbo.MovimientoEntrada");
            DropTable("dbo.ConceptoLibre");
            DropTable("dbo.ConceptoInsumo");
            DropTable("dbo.ConceptoFichaje");
            DropTable("dbo.ConceptoCuota");
            DropTable("dbo.Pago");
            DropTable("dbo.Concepto");
            DropTable("dbo.Movimiento");
        }
    }
}
