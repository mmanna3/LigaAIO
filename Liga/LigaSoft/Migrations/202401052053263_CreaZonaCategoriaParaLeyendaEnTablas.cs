namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreaZonaCategoriaParaLeyendaEnTablas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZonaCategoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZonaId = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        Leyenda = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId)
                .ForeignKey("dbo.Zona", t => t.ZonaId)
                .Index(t => new { t.ZonaId, t.CategoriaId }, unique: true, name: "IX_ZonaYCategoria");
            
            DropColumn("dbo.Categoria", "Leyenda");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categoria", "Leyenda", c => c.String());
            DropForeignKey("dbo.ZonaCategoria", "ZonaId", "dbo.Zona");
            DropForeignKey("dbo.ZonaCategoria", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.ZonaCategoria", "IX_ZonaYCategoria");
            DropTable("dbo.ZonaCategoria");
        }
    }
}
