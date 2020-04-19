using System.Web.Hosting;

namespace LigaSoft.Utilidades
{
	public abstract class AppPaths
	{
		public string ImagenesRelative { get; } = "/Imagenes";
		public string ImagenesJugadoresRelative { get; } = "/Imagenes/Jugadores";
		public string ImagenesEscudosRelative { get; } = "/Imagenes/Escudos";
		public string ImagenesPublicidadesRelative { get; } = "/Imagenes/Publicidades";

		public string ImagenesAbsolute { get; }
		public string ImagenesJugadoresAbsolute { get; }
		public string ImagenesPublicidadesAbsolute { get; }
		public string ImagenesEscudosAbsolute { get; }

		protected AppPaths()
		{
			ImagenesAbsolute = GetAbsolutePath(ImagenesRelative);
			ImagenesJugadoresAbsolute = GetAbsolutePath(ImagenesJugadoresRelative);
			ImagenesEscudosAbsolute = GetAbsolutePath(ImagenesEscudosRelative);
			ImagenesPublicidadesAbsolute = GetAbsolutePath(ImagenesPublicidadesRelative);
		}

		protected abstract string GetAbsolutePath(string relativePath);
		public abstract string BackupAbsoluteOf(string fileNameWithExtension);
		public abstract string BackupAbsolute();
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