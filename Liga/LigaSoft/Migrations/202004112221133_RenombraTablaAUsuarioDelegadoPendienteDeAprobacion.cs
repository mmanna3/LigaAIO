namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenombraTablaAUsuarioDelegadoPendienteDeAprobacion : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsuarioDelegadoSinConfirmar", newName: "UsuarioDelegadoPendienteDeAprobacion");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UsuarioDelegadoPendienteDeAprobacion", newName: "UsuarioDelegadoSinConfirmar");
        }
    }
}
