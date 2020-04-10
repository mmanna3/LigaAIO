namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NombreDelDelegadoSeRenombraADescripcion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Delegado", "Descripcion", c => c.String(nullable: false));
            DropColumn("dbo.Delegado", "Nombre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Delegado", "Nombre", c => c.String(nullable: false));
            DropColumn("dbo.Delegado", "Descripcion");
        }
    }
}
