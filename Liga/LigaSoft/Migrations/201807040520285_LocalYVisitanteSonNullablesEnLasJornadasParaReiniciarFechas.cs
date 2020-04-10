namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocalYVisitanteSonNullablesEnLasJornadasParaReiniciarFechas : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Jornada", new[] { "LocalId" });
            DropIndex("dbo.Jornada", new[] { "VisitanteId" });
            AlterColumn("dbo.Jornada", "LocalId", c => c.Int());
            AlterColumn("dbo.Jornada", "VisitanteId", c => c.Int());
            CreateIndex("dbo.Jornada", "LocalId");
            CreateIndex("dbo.Jornada", "VisitanteId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Jornada", new[] { "VisitanteId" });
            DropIndex("dbo.Jornada", new[] { "LocalId" });
            AlterColumn("dbo.Jornada", "VisitanteId", c => c.Int(nullable: false));
            AlterColumn("dbo.Jornada", "LocalId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jornada", "VisitanteId");
            CreateIndex("dbo.Jornada", "LocalId");
        }
    }
}
