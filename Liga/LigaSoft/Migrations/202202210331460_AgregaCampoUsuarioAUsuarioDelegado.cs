namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaCampoUsuarioAUsuarioDelegado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsuarioDelegado", "Usuario", c => c.String(maxLength: 30, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsuarioDelegado", "Usuario");
        }
    }
}
