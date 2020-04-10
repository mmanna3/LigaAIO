namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Partido : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Partido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JornadaId = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        GolesLocal = c.Int(nullable: false),
                        GolesVisitante = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId)
                .ForeignKey("dbo.Jornada", t => t.JornadaId)
                .Index(t => t.JornadaId)
                .Index(t => t.CategoriaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Partido", "JornadaId", "dbo.Jornada");
            DropForeignKey("dbo.Partido", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.Partido", new[] { "CategoriaId" });
            DropIndex("dbo.Partido", new[] { "JornadaId" });
            DropTable("dbo.Partido");
        }
    }
}
