namespace LigaSoft.Utilidades.Persistence.DiskPersistence
{
	public class ImagenesEscudoDiskPersistence : IImagenesEscudoPersistence
	{
		private static AppPaths Paths;

		public ImagenesEscudoDiskPersistence(AppPaths appPaths)
		{
			Paths = appPaths;
		}		
	}

	public interface IImagenesEscudoPersistence
	{
	}
}