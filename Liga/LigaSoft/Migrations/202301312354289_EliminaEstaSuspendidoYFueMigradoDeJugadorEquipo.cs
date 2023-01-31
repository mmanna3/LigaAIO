namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminaEstaSuspendidoYFueMigradoDeJugadorEquipo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.JugadorEquipo", "FueMigrado");
            DropColumn("dbo.JugadorEquipo", "EstaSuspendido");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JugadorEquipo", "EstaSuspendido", c => c.Boolean(nullable: false));
            AddColumn("dbo.JugadorEquipo", "FueMigrado", c => c.Boolean(nullable: false));
        }
    }
}
