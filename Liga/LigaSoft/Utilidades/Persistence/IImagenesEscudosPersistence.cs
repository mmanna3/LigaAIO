using LigaSoft.Models.ViewModels;

namespace LigaSoft.Utilidades.Persistence
{
	public interface IImagenesEscudosPersistence
	{
		string Path(int clubId);
		void Eliminar(int clubId);
		void Guardar(CargarEscudoVM vm);
		void GuardarEscudoDefault(string escudoBase64);
	}
}