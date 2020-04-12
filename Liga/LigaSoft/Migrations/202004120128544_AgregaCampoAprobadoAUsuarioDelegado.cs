namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaCampoAprobadoAUsuarioDelegado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsuarioDelegado", "Aprobado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsuarioDelegado", "Aprobado");
        }
    }
}
