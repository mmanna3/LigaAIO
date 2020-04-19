namespace LigaSoft.Utilidades.Persistence
{
	public interface IBackupPersistence
	{
		string ComprimirImagenesYPonerZipEnCarpetaDeBackups();		
		string ComprimirUltimoBackupBdYPonerZipEnCarpetaDeBackups();
		void EliminarTodosLosArchivosDeLaCarpetaDondeEstanLosBackups();
	}
}