using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EfEnumToLookup.LookupGenerator;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Dominio.Finanzas;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LigaSoft.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
	    [InverseProperty("UsuarioAlta")]
		public virtual ICollection<Movimiento> MovimientosQueDioDeAlta { get; set; }

	    [InverseProperty("UsuarioAnulacion")]
	    public virtual ICollection<Movimiento> MovimientosQueAnulo { get; set; }

	    [InverseProperty("UsuarioAlta")]
	    public virtual ICollection<Pago> PagosQueDioDeAlta { get; set; }

	    [InverseProperty("UsuarioAnulacion")]
	    public virtual ICollection<Pago> PagosQueAnulo { get; set; }


		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
	    public DbSet<Club> Clubs { get; set; }
	    public DbSet<Equipo> Equipos { get; set; }
	    public DbSet<Jugador> Jugadores { get; set; }
	    public DbSet<Torneo> Torneos { get; set; }
	    public DbSet<TorneoTipo> TorneoTipos { get; set; }
	    public DbSet<JugadorEquipo> JugadorEquipos { get; set; }
	    public DbSet<Zona> Zonas { get; set; }
	    public DbSet<Categoria> Categorias { get; set; }
		public DbSet<QuitaDePuntos> QuitaDePuntos { get; set; }
		public DbSet<ZonaCategoria> ZonaCategorias { get; set; }
	    public DbSet<Fecha> Fechas { get; set; }
	    public DbSet<Jornada> Jornadas { get; set; }
	    public DbSet<Partido> Partidos { get; set; }
	    public DbSet<Delegado> Delegados { get; set; }
	    public DbSet<ZonaRelampagoEquipo> ZonaRelampagoEquipos { get; set; }
	    public DbSet<Noticia> Noticias { get; set; }
        public DbSet<ParametroGlobal> ParametrizacionesGlobales { get; set; }
	    public DbSet<Goleador> Goleadores { get; set; }
	    public DbSet<Publicidad> Publicidades { get; set; }
	    public DbSet<Sancion> Sanciones { get; set; }
	    public DbSet<UsuarioDelegado> UsuariosDelegados { get; set; }
	    //public DbSet<JugadorFichadoPorDelegado> JugadoresFichadosPorDelegados { get; set; }
	    public DbSet<JugadorAutofichado> JugadoresaAutofichados { get; set; }

		//Finanzas
		public DbSet<Movimiento> Movimientos { get; set; }
	    public DbSet<MovimientoEntradaSinClub> MovimientosEntradaSinClub { get; set; }
		public DbSet<MovimientoEntradaConClub> MovimientosEntradaConClub { get; set; }
	    public DbSet<MovimientoEntradaConClubCuota> MovimientosEntradaConClubCuota { get; set; }
		public DbSet<MovimientoSalida> MovimientosSalida { get; set; }
	    public DbSet<Pago> Pagos { get; set; }
	    public DbSet<Concepto> Conceptos { get; set; }
	    public DbSet<ConceptoCuota> ConceptosCuota { get; set; }
	    public DbSet<ConceptoFichaje> ConceptosFichaje { get; set; }
	    public DbSet<ConceptoInsumo> ConceptosInsumo { get; set; }
	    public DbSet<ConceptoLibre> ConceptosLibre { get; set; }
		public DbSet<JugadorSancionadoDePorVida> JugadoresSancionadosDePorVida { get; set; }

		public IQueryable<Equipo> EquiposActivos()
		{
			return this.Equipos.Where(x => !x.BajaLogica);
		}

		public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
		{		
		}

		public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

	    protected override void OnModelCreating(DbModelBuilder modelBuilder)
	    {
		    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


			modelBuilder.Entity<Jornada>()
			    .HasOptional(m => m.Local)
			    .WithMany(t => t.JornadasDeLocal)
			    .HasForeignKey(m => m.LocalId)
			    .WillCascadeOnDelete(false);

		    modelBuilder.Entity<Jornada>()
			    .HasOptional(m => m.Visitante)
			    .WithMany(t => t.JornadasDeVisitante)
			    .HasForeignKey(m => m.VisitanteId)
			    .WillCascadeOnDelete(false);

			base.OnModelCreating(modelBuilder);
		}
    }
}