namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaCampoEstaSuspendidoAJugadorEquipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JugadorEquipo", "EstaSuspendido", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JugadorEquipo", "EstaSuspendido");
        }
    }
}
