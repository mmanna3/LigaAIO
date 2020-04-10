namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgregaElEscudoEnLosClubs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Club", "EscudoBase64", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Club", "EscudoBase64");
        }
    }
}
