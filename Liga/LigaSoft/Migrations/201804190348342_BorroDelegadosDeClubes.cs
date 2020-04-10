namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BorroDelegadosDeClubes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Club", "Localidad", c => c.String());
            AlterColumn("dbo.Club", "Direccion", c => c.String());
            DropColumn("dbo.Club", "Delegado");
            DropColumn("dbo.Club", "Telefono");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Club", "Telefono", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Club", "Delegado", c => c.String(nullable: false));
            AlterColumn("dbo.Club", "Direccion", c => c.String(nullable: false));
            AlterColumn("dbo.Club", "Localidad", c => c.String(nullable: false));
        }
    }
}
