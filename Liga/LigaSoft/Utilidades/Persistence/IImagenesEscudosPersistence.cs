using LigaSoft.Models.ViewModels;

namespace LigaSoft.Utilidades.Persistence
{
	public interface IImagenesEscudosPersistence
	{
		string Path(int clubId, string escudoPorDefecto);
		void Eliminar(int id);
		void Guardar(CargarEscudoVM vm);
	}
}