namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ElEquipoPuedeSerNullParaContemplarEquipoLibre : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PartidoEliminacionDirecta", new[] { "LocalId" });
            DropIndex("dbo.PartidoEliminacionDirecta", new[] { "VisitanteId" });
            AlterColumn("dbo.PartidoEliminacionDirecta", "LocalId", c => c.Int());
            AlterColumn("dbo.PartidoEliminacionDirecta", "VisitanteId", c => c.Int());
            CreateIndex("dbo.PartidoEliminacionDirecta", "LocalId");
            CreateIndex("dbo.PartidoEliminacionDirecta", "VisitanteId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PartidoEliminacionDirecta", new[] { "VisitanteId" });
            DropIndex("dbo.PartidoEliminacionDirecta", new[] { "LocalId" });
            AlterColumn("dbo.PartidoEliminacionDirecta", "VisitanteId", c => c.Int(nullable: false));
            AlterColumn("dbo.PartidoEliminacionDirecta", "LocalId", c => c.Int(nullable: false));
            CreateIndex("dbo.PartidoEliminacionDirecta", "VisitanteId");
            CreateIndex("dbo.PartidoEliminacionDirecta", "LocalId");
        }
    }
}
