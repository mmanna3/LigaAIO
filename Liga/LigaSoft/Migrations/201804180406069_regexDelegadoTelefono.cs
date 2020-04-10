namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class regexDelegadoTelefono : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Delegado", "Telefono", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Delegado", "Telefono", c => c.String(nullable: false));
        }
    }
}
