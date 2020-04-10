namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClaveIntAParametrizacionGlobal : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ParametrizacionGlobal");
            AddColumn("dbo.ParametrizacionGlobal", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ParametrizacionGlobal", "Clave", c => c.String());
            AddPrimaryKey("dbo.ParametrizacionGlobal", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ParametrizacionGlobal");
            AlterColumn("dbo.ParametrizacionGlobal", "Clave", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.ParametrizacionGlobal", "Id");
            AddPrimaryKey("dbo.ParametrizacionGlobal", "Clave");
        }
    }
}
