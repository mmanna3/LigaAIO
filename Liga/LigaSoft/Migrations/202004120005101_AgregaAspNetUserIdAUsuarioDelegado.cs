namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaAspNetUserIdAUsuarioDelegado : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsuarioDelegadoPendienteDeAprobacion", newName: "UsuarioDelegado");
            AddColumn("dbo.UsuarioDelegado", "AspNetUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UsuarioDelegado", "AspNetUserId");
            AddForeignKey("dbo.UsuarioDelegado", "AspNetUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioDelegado", "AspNetUserId", "dbo.AspNetUsers");
            DropIndex("dbo.UsuarioDelegado", new[] { "AspNetUserId" });
            DropColumn("dbo.UsuarioDelegado", "AspNetUserId");
            RenameTable(name: "dbo.UsuarioDelegado", newName: "UsuarioDelegadoPendienteDeAprobacion");
        }
    }
}
