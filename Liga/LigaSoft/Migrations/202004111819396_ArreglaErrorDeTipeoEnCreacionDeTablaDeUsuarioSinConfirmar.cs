namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArreglaErrorDeTipeoEnCreacionDeTablaDeUsuarioSinConfirmar : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsusarioDelegadoSinConfirmar", newName: "UsuarioDelegadoSinConfirmar");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UsuarioDelegadoSinConfirmar", newName: "UsusarioDelegadoSinConfirmar");
        }
    }
}
