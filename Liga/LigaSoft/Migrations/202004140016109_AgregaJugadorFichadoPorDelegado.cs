namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaJugadorFichadoPorDelegado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JugadorFichadoPorDelegado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DNI = c.String(nullable: false, maxLength: 9, unicode: false),
                        Nombre = c.String(maxLength: 14, unicode: false),
                        Apellido = c.String(maxLength: 14, unicode: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.DNI, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.JugadorFichadoPorDelegado", new[] { "DNI" });
            DropTable("dbo.JugadorFichadoPorDelegado");
        }
    }
}
