namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameParametroGlobalEscudoEnBase64 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParametroGlobal", "EscudoPorDefectoEnBase64", c => c.String());
            DropColumn("dbo.ParametroGlobal", "EscudoPorDefecto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ParametroGlobal", "EscudoPorDefecto", c => c.String());
            DropColumn("dbo.ParametroGlobal", "EscudoPorDefectoEnBase64");
        }
    }
}
