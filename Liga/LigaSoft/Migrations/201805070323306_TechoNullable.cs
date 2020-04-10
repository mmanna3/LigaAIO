namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TechoNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Club", "Techo", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Club", "Techo", c => c.Boolean(nullable: false));
        }
    }
}
