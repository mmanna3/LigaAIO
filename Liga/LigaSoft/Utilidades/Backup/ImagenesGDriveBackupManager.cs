namespace LigaSoft.Utilidades.Backup
{
	public class ImagenesGDriveBackupManager : GDriveBackupManager
	{
		protected override string ComprimirYPonerZipEnAppData()
		{
			return BackupDiskPersistence.ComprimirImagenesYPonerZipEnCarpetaDeBackups();
		}

		protected override string NombreDelBackupZipeadoSinExtensionNiFecha()
		{
			return "Imagenes";
		}
	}
}