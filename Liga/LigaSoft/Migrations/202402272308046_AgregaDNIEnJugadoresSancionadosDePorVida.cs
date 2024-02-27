namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaDNIEnJugadoresSancionadosDePorVida : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JugadorSancionadoDePorVida", "DNI", c => c.String(nullable: false, maxLength: 9, unicode: false));
            CreateIndex("dbo.JugadorSancionadoDePorVida", "DNI", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.JugadorSancionadoDePorVida", new[] { "DNI" });
            DropColumn("dbo.JugadorSancionadoDePorVida", "DNI");
        }
    }
}
