namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameParametroGlobal : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ParametrizacionGlobal", newName: "ParametroGlobal");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ParametroGlobal", newName: "ParametrizacionGlobal");
        }
    }
}
