using LigaSoft.Models.ViewModels;

namespace LigaSoft.Utilidades.Persistence
{
	public interface IImagenesEscudosPersistence
	{
		string PathRelativo(int clubId);
		string PathRelativoDelEscudoDefault();
		void Eliminar(int clubId);
		void Guardar(CargarEscudoVM vm);
		void GuardarEscudoDefault(string escudoBase64);
	}
}