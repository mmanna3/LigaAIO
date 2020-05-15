namespace LigaSoft.Utilidades.Persistence
{
	public interface IBackupPersistence
	{
		string ComprimirImagenesYPonerZipEnCarpetaDeBackups();		
		string ComprimirBackupBaseDeDatosYPonerZipEnCarpetaDeBackups();
		void EliminarTodosLosArchivosDeLaCarpetaDondeEstanLosBackups();
		void GenerarBackupDeBaseDeDatosEnCarpetaTemporal();
	}
}