namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuitaJugadoresFichadosPorDelegados : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JugadorFichadoPorDelegado", "EquipoId", "dbo.Equipo");
            DropIndex("dbo.JugadorFichadoPorDelegado", new[] { "DNI" });
            DropIndex("dbo.JugadorFichadoPorDelegado", new[] { "EquipoId" });
            DropTable("dbo.JugadorFichadoPorDelegado");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.JugadorFichadoPorDelegado",
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.JugadorFichadoPorDelegado", "EquipoId");
            CreateIndex("dbo.JugadorFichadoPorDelegado", "DNI", unique: true);
            AddForeignKey("dbo.JugadorFichadoPorDelegado", "EquipoId", "dbo.Equipo", "Id");
        }
    }
}
