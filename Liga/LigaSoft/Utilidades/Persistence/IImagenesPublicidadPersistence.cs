using LigaSoft.Models.ViewModels;

namespace LigaSoft.Utilidades.Persistence
{
	public interface IImagenesPublicidadPersistence
	{
		string Path(int id);
		void Guardar(PublicidadVM vm);
	}
}