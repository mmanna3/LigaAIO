namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndiceEnPartidoPorJornadaYCategoria : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Partido", new[] { "JornadaId" });
            DropIndex("dbo.Partido", new[] { "CategoriaId" });
            CreateIndex("dbo.Partido", new[] { "JornadaId", "CategoriaId" }, unique: true, name: "IX_JornadaYCategoria");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Partido", "IX_JornadaYCategoria");
            CreateIndex("dbo.Partido", "CategoriaId");
            CreateIndex("dbo.Partido", "JornadaId");
        }
    }
}
