namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaJugadoresSancionadosDePorVida : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JugadorSancionadoDePorVida",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 14),
                        Apellido = c.String(nullable: false, maxLength: 14),
                        Leyenda = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JugadorSancionadoDePorVida");
        }
    }
}
