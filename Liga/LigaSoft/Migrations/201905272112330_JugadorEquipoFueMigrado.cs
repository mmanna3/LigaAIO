namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JugadorEquipoFueMigrado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JugadorEquipo", "FueMigrado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JugadorEquipo", "FueMigrado");
        }
    }
}
