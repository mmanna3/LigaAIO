namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgregaSancion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sancion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JornadaId = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 300),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId)
                .ForeignKey("dbo.Jornada", t => t.JornadaId)
                .Index(t => t.JornadaId)
                .Index(t => t.CategoriaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sancion", "JornadaId", "dbo.Jornada");
            DropForeignKey("dbo.Sancion", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.Sancion", new[] { "CategoriaId" });
            DropIndex("dbo.Sancion", new[] { "JornadaId" });
            DropTable("dbo.Sancion");
        }
    }
}
