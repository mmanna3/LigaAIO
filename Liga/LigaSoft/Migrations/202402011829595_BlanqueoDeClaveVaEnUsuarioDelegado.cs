namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlanqueoDeClaveVaEnUsuarioDelegado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsuarioDelegado", "BlanqueoDeClavePendiente", c => c.Boolean(nullable: false));
            DropColumn("dbo.Delegado", "BlanqueoDeClavePendiente");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Delegado", "BlanqueoDeClavePendiente", c => c.Boolean(nullable: false));
            DropColumn("dbo.UsuarioDelegado", "BlanqueoDeClavePendiente");
        }
    }
}
