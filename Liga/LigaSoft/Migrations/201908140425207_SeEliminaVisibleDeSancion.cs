namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeEliminaVisibleDeSancion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sancion", "Visible");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sancion", "Visible", c => c.Boolean(nullable: false));
        }
    }
}
