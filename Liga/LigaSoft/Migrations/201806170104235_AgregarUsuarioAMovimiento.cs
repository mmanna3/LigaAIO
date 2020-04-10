namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarUsuarioAMovimiento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movimiento", "UsuarioId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Movimiento", "UsuarioId");
            AddForeignKey("dbo.Movimiento", "UsuarioId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movimiento", "UsuarioId", "dbo.AspNetUsers");
            DropIndex("dbo.Movimiento", new[] { "UsuarioId" });
            DropColumn("dbo.Movimiento", "UsuarioId");
        }
    }
}
