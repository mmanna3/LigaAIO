namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizaIndiceEnZonaCategoria : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ZonaCategoria", "IX_ZonaYCategoria");
            CreateIndex("dbo.ZonaCategoria", new[] { "ZonaId", "CategoriaId", "EsAnual" }, unique: true, name: "IX_ZonaYCategoria");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ZonaCategoria", "IX_ZonaYCategoria");
            CreateIndex("dbo.ZonaCategoria", new[] { "ZonaId", "CategoriaId" }, unique: true, name: "IX_ZonaYCategoria");
        }
    }
}
