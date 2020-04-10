namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminoFotoDeJugadorDeLaBaseParaPasarlaAlDisco : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jugador", "FotoBase64");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jugador", "FotoBase64", c => c.String());
        }
    }
}
