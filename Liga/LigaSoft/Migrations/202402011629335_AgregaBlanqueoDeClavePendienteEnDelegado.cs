namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaBlanqueoDeClavePendienteEnDelegado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Delegado", "BlanqueoDeClavePendiente", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Delegado", "BlanqueoDeClavePendiente");
        }
    }
}
