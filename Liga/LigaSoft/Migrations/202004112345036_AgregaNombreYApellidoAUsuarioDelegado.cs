namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaNombreYApellidoAUsuarioDelegado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsuarioDelegadoPendienteDeAprobacion", "Nombre", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.UsuarioDelegadoPendienteDeAprobacion", "Apellido", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.UsuarioDelegadoPendienteDeAprobacion", "Email", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.UsuarioDelegadoPendienteDeAprobacion", "Password", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UsuarioDelegadoPendienteDeAprobacion", "Password", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.UsuarioDelegadoPendienteDeAprobacion", "Email", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.UsuarioDelegadoPendienteDeAprobacion", "Apellido");
            DropColumn("dbo.UsuarioDelegadoPendienteDeAprobacion", "Nombre");
        }
    }
}
