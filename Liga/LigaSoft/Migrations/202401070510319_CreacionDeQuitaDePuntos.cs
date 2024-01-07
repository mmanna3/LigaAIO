namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionDeQuitaDePuntos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuitaDePuntos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZonaId = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        EquipoId = c.Int(nullable: false),
                        CantidadDePuntosDescontados = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId)
                .ForeignKey("dbo.Equipo", t => t.EquipoId)
                .ForeignKey("dbo.Zona", t => t.ZonaId)
                .Index(t => new { t.ZonaId, t.CategoriaId, t.EquipoId }, unique: true, name: "IX_ZonaYCategoriaYEquipo");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuitaDePuntos", "ZonaId", "dbo.Zona");
            DropForeignKey("dbo.QuitaDePuntos", "EquipoId", "dbo.Equipo");
            DropForeignKey("dbo.QuitaDePuntos", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.QuitaDePuntos", "IX_ZonaYCategoriaYEquipo");
            DropTable("dbo.QuitaDePuntos");
        }
    }
}
