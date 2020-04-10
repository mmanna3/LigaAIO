namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PagosAuditoria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pago", "FechaAlta", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pago", "UsuarioAltaId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Pago", "FechaAnulacion", c => c.DateTime());
            AddColumn("dbo.Pago", "UsuarioAnulacionId", c => c.String(maxLength: 128));
            AddColumn("dbo.Pago", "Comentario", c => c.String());
            AddColumn("dbo.Pago", "Vigente", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Pago", "UsuarioAltaId");
            CreateIndex("dbo.Pago", "UsuarioAnulacionId");
            AddForeignKey("dbo.Pago", "UsuarioAnulacionId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Pago", "UsuarioAltaId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pago", "UsuarioAltaId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pago", "UsuarioAnulacionId", "dbo.AspNetUsers");
            DropIndex("dbo.Pago", new[] { "UsuarioAnulacionId" });
            DropIndex("dbo.Pago", new[] { "UsuarioAltaId" });
            DropColumn("dbo.Pago", "Vigente");
            DropColumn("dbo.Pago", "Comentario");
            DropColumn("dbo.Pago", "UsuarioAnulacionId");
            DropColumn("dbo.Pago", "FechaAnulacion");
            DropColumn("dbo.Pago", "UsuarioAltaId");
            DropColumn("dbo.Pago", "FechaAlta");
        }
    }
}
