namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaOrdenAlIndiceUnicoDePartidosEliminacionDirecta : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PartidoEliminacionDirecta", "IX_TorneoYCategoriaYFase");
            CreateIndex("dbo.PartidoEliminacionDirecta", new[] { "TorneoId", "CategoriaId", "Fase", "Orden" }, unique: true, name: "IX_TorneoYCategoriaYFaseYOrden");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PartidoEliminacionDirecta", "IX_TorneoYCategoriaYFaseYOrden");
            CreateIndex("dbo.PartidoEliminacionDirecta", new[] { "TorneoId", "CategoriaId", "Fase" }, unique: true, name: "IX_TorneoYCategoriaYFase");
        }
    }
}
