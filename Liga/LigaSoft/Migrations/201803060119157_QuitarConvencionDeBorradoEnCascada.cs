namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuitarConvencionDeBorradoEnCascada : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categoria", "TorneoId", "dbo.Torneo");
            DropForeignKey("dbo.Equipo", "TorneoId", "dbo.Torneo");
            DropForeignKey("dbo.Torneo", "TipoId", "dbo.TorneoTipo");
            DropForeignKey("dbo.Zona", "TorneoId", "dbo.Torneo");
            DropForeignKey("dbo.Equipo", "ClubId", "dbo.Club");
            DropForeignKey("dbo.JugadorEquipo", "EquipoId", "dbo.Equipo");
            DropForeignKey("dbo.LocalVisitante", "FechaId", "dbo.Fecha");
            DropForeignKey("dbo.Fecha", "ZonaId", "dbo.Zona");
            DropForeignKey("dbo.JugadorEquipo", "JugadorId", "dbo.Jugador");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Categoria", "TorneoId", "dbo.Torneo", "Id");
            AddForeignKey("dbo.Equipo", "TorneoId", "dbo.Torneo", "Id");
            AddForeignKey("dbo.Torneo", "TipoId", "dbo.TorneoTipo", "Id");
            AddForeignKey("dbo.Zona", "TorneoId", "dbo.Torneo", "Id");
            AddForeignKey("dbo.Equipo", "ClubId", "dbo.Club", "Id");
            AddForeignKey("dbo.JugadorEquipo", "EquipoId", "dbo.Equipo", "Id");
            AddForeignKey("dbo.LocalVisitante", "FechaId", "dbo.Fecha", "Id");
            AddForeignKey("dbo.Fecha", "ZonaId", "dbo.Zona", "Id");
            AddForeignKey("dbo.JugadorEquipo", "JugadorId", "dbo.Jugador", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.JugadorEquipo", "JugadorId", "dbo.Jugador");
            DropForeignKey("dbo.Fecha", "ZonaId", "dbo.Zona");
            DropForeignKey("dbo.LocalVisitante", "FechaId", "dbo.Fecha");
            DropForeignKey("dbo.JugadorEquipo", "EquipoId", "dbo.Equipo");
            DropForeignKey("dbo.Equipo", "ClubId", "dbo.Club");
            DropForeignKey("dbo.Zona", "TorneoId", "dbo.Torneo");
            DropForeignKey("dbo.Torneo", "TipoId", "dbo.TorneoTipo");
            DropForeignKey("dbo.Equipo", "TorneoId", "dbo.Torneo");
            DropForeignKey("dbo.Categoria", "TorneoId", "dbo.Torneo");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JugadorEquipo", "JugadorId", "dbo.Jugador", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Fecha", "ZonaId", "dbo.Zona", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LocalVisitante", "FechaId", "dbo.Fecha", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JugadorEquipo", "EquipoId", "dbo.Equipo", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Equipo", "ClubId", "dbo.Club", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Zona", "TorneoId", "dbo.Torneo", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Torneo", "TipoId", "dbo.TorneoTipo", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Equipo", "TorneoId", "dbo.Torneo", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Categoria", "TorneoId", "dbo.Torneo", "Id", cascadeDelete: true);
        }
    }
}
