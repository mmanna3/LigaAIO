namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HaceRequeridosAlgunosAtributosDeJugadorFichadoPorDelegado : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JugadorFichadoPorDelegado", "Nombre", c => c.String(nullable: false, maxLength: 14, unicode: false));
            AlterColumn("dbo.JugadorFichadoPorDelegado", "Apellido", c => c.String(nullable: false, maxLength: 14, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JugadorFichadoPorDelegado", "Apellido", c => c.String(maxLength: 14, unicode: false));
            AlterColumn("dbo.JugadorFichadoPorDelegado", "Nombre", c => c.String(maxLength: 14, unicode: false));
        }
    }
}
