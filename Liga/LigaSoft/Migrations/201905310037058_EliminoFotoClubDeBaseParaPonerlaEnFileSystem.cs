namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminoFotoClubDeBaseParaPonerlaEnFileSystem : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Club", "EscudoBase64");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Club", "EscudoBase64", c => c.String());
        }
    }
}
