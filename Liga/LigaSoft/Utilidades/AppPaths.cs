using System.Web.Hosting;

namespace LigaSoft.Utilidades
{
	public abstract class AppPaths
	{
		public string ImagenesRelative { get; } = "/Imagenes";
		public string ImagenesJugadoresRelative { get; } = "/Imagenes/Jugadores";
		public string ImagenesEscudosRelative { get; } = "/Imagenes/Escudos";
		public string ImagenesPublicidadesRelative { get; } = "/Imagenes/Publicidades";
		public string EscudoDefaultRelative { get; } = "/Imagenes/Escudos/default.jpg";
		public string ImagenesTemporalesJugadorCarnetRelative { get; } = "/Imagenes/Temporales/Carnet";
		public string ImagenesTemporalesJugadorDNIFrenteRelative { get; } = "/Imagenes/Temporales/DNIFrente";

		public string ImagenesAbsolute { get; }
		public string ImagenesJugadoresAbsolute { get; }
		public string ImagenesPublicidadesAbsolute { get; }
		public string ImagenesEscudosAbsolute { get; }
		public string EscudoDefaultFileAbsolute { get; }
		public string ImagenesTemporalesJugadorCarnetAbsolute { get; set; }
		public string ImagenesTemporalesJugadorDNIFrenteAbsolute { get; set; }

		// ReSharper disable VirtualMemberCallInConstructor
		protected AppPaths()
		{			
			ImagenesAbsolute = GetAbsolutePath(ImagenesRelative);
			ImagenesJugadoresAbsolute = GetAbsolutePath(ImagenesJugadoresRelative);
			ImagenesEscudosAbsolute = GetAbsolutePath(ImagenesEscudosRelative);
			ImagenesPublicidadesAbsolute = GetAbsolutePath(ImagenesPublicidadesRelative);
			EscudoDefaultFileAbsolute = GetAbsolutePath(EscudoDefaultRelative);
			ImagenesTemporalesJugadorCarnetAbsolute = GetAbsolutePath(ImagenesTemporalesJugadorCarnetRelative);
			ImagenesTemporalesJugadorDNIFrenteAbsolute = GetAbsolutePath(ImagenesTemporalesJugadorDNIFrenteRelative);
		}

		protected abstract string GetAbsolutePath(string relativePath);
		public abstract string BackupAbsoluteOf(string fileNameWithExtension);
		public abstract string BackupAbsolute();

		public string BackupImagenes()
		{
			var fileName = $"Imagenes-{DateTimeUtils.NowInArgentinaBackupFormat}.zip";
			return BackupAbsoluteOf(fileName);
		}

		public string BackupBaseDeDatos()
		{
			var fileName = $"BaseDeDatos-{DateTimeUtils.NowInArgentinaBackupFormat}.zip";
			return BackupAbsoluteOf(fileName);
		}
	}

	public class AppPathsWebApp : AppPaths
	{
		protected override string GetAbsolutePath(string relativePath)
		{
			return HostingEnvironment.MapPath($"~{relativePath}");
		}

		public override string BackupAbsoluteOf(string fileNameWithExtension)
		{
			return HostingEnvironment.MapPath($"~/App_Data/{fileNameWithExtension}");
		}

		public override string BackupAbsolute()
		{
			return HostingEnvironment.MapPath("~/App_Data/");
		}
	}
		
}