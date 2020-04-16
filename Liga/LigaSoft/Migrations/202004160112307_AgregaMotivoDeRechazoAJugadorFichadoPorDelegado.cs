namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaMotivoDeRechazoAJugadorFichadoPorDelegado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JugadorFichadoPorDelegado", "MotivoDeRechazo", c => c.String(maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JugadorFichadoPorDelegado", "MotivoDeRechazo");
        }
    }
}
