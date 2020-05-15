namespace LigaSoft.Utilidades.Backup
{
	public class BaseDeDatosGDriveBackupManager : GDriveBackupManager
	{
		protected override string ComprimirYPonerZipEnAppData()
		{
			BackupDiskPersistence.GenerarBackupDeBaseDeDatosEnCarpetaTemporal();
			return BackupDiskPersistence.ComprimirBackupBaseDeDatosYPonerZipEnCarpetaDeBackups();
		}

		protected override string NombreDelBackupZipeadoSinExtensionNiFecha()
		{
			return "BaseDeDatos";
		}
	}
}