namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimientoSalidaSinProveedor : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MovimientoSalida", "Proveedor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MovimientoSalida", "Proveedor", c => c.String());
        }
    }
}
