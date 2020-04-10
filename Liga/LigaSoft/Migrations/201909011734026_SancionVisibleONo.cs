namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SancionVisibleONo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sancion", "Visible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sancion", "Visible");
        }
    }
}
