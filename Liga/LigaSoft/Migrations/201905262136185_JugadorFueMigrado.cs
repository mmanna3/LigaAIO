namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JugadorFueMigrado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jugador", "FueMigrado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jugador", "FueMigrado");
        }
    }
}
