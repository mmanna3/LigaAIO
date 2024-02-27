namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiaLeyendaPorMotivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JugadorSancionadoDePorVida", "Motivo", c => c.String(maxLength: 300));
            DropColumn("dbo.JugadorSancionadoDePorVida", "Leyenda");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JugadorSancionadoDePorVida", "Leyenda", c => c.String(maxLength: 300));
            DropColumn("dbo.JugadorSancionadoDePorVida", "Motivo");
        }
    }
}
