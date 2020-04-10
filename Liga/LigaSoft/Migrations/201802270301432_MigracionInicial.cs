namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigracionInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Club",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Localidad = c.String(nullable: false),
                        Direccion = c.String(nullable: false),
                        Delegado = c.String(nullable: false),
                        Techo = c.Boolean(nullable: false),
                        Telefono = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Equipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 18),
                        ClubId = c.Int(nullable: false),
                        TorneoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Club", t => t.ClubId, cascadeDelete: true)
                .ForeignKey("dbo.Torneo", t => t.TorneoId, cascadeDelete: true)
                .Index(t => t.Nombre, unique: true)
                .Index(t => t.ClubId)
                .Index(t => t.TorneoId);
            
            CreateTable(
                "dbo.JugadorEquipo",
                c => new
                    {
                        JugadorId = c.Int(nullable: false),
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JugadorId, t.EquipoId })
                .ForeignKey("dbo.Equipo", t => t.EquipoId, cascadeDelete: true)
                .ForeignKey("dbo.Jugador", t => t.JugadorId, cascadeDelete: true)
                .Index(t => t.JugadorId)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.Jugador",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DNI = c.String(nullable: false, maxLength: 14),
                        Nombre = c.String(maxLength: 14),
                        Apellido = c.String(maxLength: 14),
                        FechaNacimiento = c.DateTime(nullable: false),
                        FotoBase64 = c.String(),
                        CarnetImpreso = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.DNI, unique: true);
            
            CreateTable(
                "dbo.Torneo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Anio = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        Activa = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TorneoTipo", t => t.TipoId, cascadeDelete: true)
                .Index(t => new { t.Anio, t.TipoId }, unique: true, name: "IX_AnioYTipo");
            
            CreateTable(
                "dbo.TorneoTipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        ValidezDelCarnetEnAnios = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Torneo", "TipoId", "dbo.TorneoTipo");
            DropForeignKey("dbo.Equipo", "TorneoId", "dbo.Torneo");
            DropForeignKey("dbo.JugadorEquipo", "JugadorId", "dbo.Jugador");
            DropForeignKey("dbo.JugadorEquipo", "EquipoId", "dbo.Equipo");
            DropForeignKey("dbo.Equipo", "ClubId", "dbo.Club");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Torneo", "IX_AnioYTipo");
            DropIndex("dbo.Jugador", new[] { "DNI" });
            DropIndex("dbo.JugadorEquipo", new[] { "EquipoId" });
            DropIndex("dbo.JugadorEquipo", new[] { "JugadorId" });
            DropIndex("dbo.Equipo", new[] { "TorneoId" });
            DropIndex("dbo.Equipo", new[] { "ClubId" });
            DropIndex("dbo.Equipo", new[] { "Nombre" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TorneoTipo");
            DropTable("dbo.Torneo");
            DropTable("dbo.Jugador");
            DropTable("dbo.JugadorEquipo");
            DropTable("dbo.Equipo");
            DropTable("dbo.Club");
        }
    }
}
