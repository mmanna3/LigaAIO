namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaEquipoAJugadorFichadoPorDelegado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JugadorFichadoPorDelegado", "EquipoId", c => c.Int(nullable: false));
            CreateIndex("dbo.JugadorFichadoPorDelegado", "EquipoId");
            AddForeignKey("dbo.JugadorFichadoPorDelegado", "EquipoId", "dbo.Equipo", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JugadorFichadoPorDelegado", "EquipoId", "dbo.Equipo");
            DropIndex("dbo.JugadorFichadoPorDelegado", new[] { "EquipoId" });
            DropColumn("dbo.JugadorFichadoPorDelegado", "EquipoId");
        }
    }
}
