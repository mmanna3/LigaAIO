namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZonaTipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zona", "Tipo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Zona", "Tipo");
        }
    }
}
