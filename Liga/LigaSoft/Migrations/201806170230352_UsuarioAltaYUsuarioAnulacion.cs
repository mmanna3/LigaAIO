namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsuarioAltaYUsuarioAnulacion : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Movimiento", new[] { "UsuarioId" });
            RenameColumn(table: "dbo.Movimiento", name: "UsuarioId", newName: "UsuarioAnulacionId");
            AddColumn("dbo.Movimiento", "FechaAlta", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movimiento", "UsuarioAltaId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Movimiento", "FechaAnulacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Movimiento", "UsuarioAnulacionId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Movimiento", "UsuarioAltaId");
            CreateIndex("dbo.Movimiento", "UsuarioAnulacionId");
            AddForeignKey("dbo.Movimiento", "UsuarioAltaId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movimiento", "UsuarioAltaId", "dbo.AspNetUsers");
            DropIndex("dbo.Movimiento", new[] { "UsuarioAnulacionId" });
            DropIndex("dbo.Movimiento", new[] { "UsuarioAltaId" });
            AlterColumn("dbo.Movimiento", "UsuarioAnulacionId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Movimiento", "FechaAnulacion");
            DropColumn("dbo.Movimiento", "UsuarioAltaId");
            DropColumn("dbo.Movimiento", "FechaAlta");
            RenameColumn(table: "dbo.Movimiento", name: "UsuarioAnulacionId", newName: "UsuarioId");
            CreateIndex("dbo.Movimiento", "UsuarioId");
        }
    }
}
