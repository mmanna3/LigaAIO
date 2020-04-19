namespace LigaSoft.Utilidades.Backup
{
	public class BaseDeDatosGDriveBackupManager : GDriveBackupManager
	{
		protected override string ComprimirYPonerZipEnAppData()
		{
			return BackupDiskPersistence.ComprimirUltimoBackupBdYPonerZipEnCarpetaDeBackups();
		}

		protected override string NombreDelBackupZipeadoSinExtensionNiFecha()
		{
			return "BaseDeDatos";
		}
	}
}