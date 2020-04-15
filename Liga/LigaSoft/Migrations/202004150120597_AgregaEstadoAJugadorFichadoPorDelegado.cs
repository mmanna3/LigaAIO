namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaEstadoAJugadorFichadoPorDelegado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JugadorFichadoPorDelegado", "Estado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JugadorFichadoPorDelegado", "Estado");
        }
    }
}
