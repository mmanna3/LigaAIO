namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaJugadorAutofichado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JugadorAutofichado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DNI = c.String(nullable: false, maxLength: 9, unicode: false),
                        Nombre = c.String(nullable: false, maxLength: 14, unicode: false),
                        Apellido = c.String(nullable: false, maxLength: 14, unicode: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                        EquipoId = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                        MotivoDeRechazo = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipo", t => t.EquipoId)
                .Index(t => t.DNI, unique: true)
                .Index(t => t.EquipoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JugadorAutofichado", "EquipoId", "dbo.Equipo");
            DropIndex("dbo.JugadorAutofichado", new[] { "EquipoId" });
            DropIndex("dbo.JugadorAutofichado", new[] { "DNI" });
            DropTable("dbo.JugadorAutofichado");
        }
    }
}
