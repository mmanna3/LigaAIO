namespace LigaSoft.Utilidades.Backup
{
	public class BaseDeDatosGDriveBackupManager : GDriveBackupManager
	{
		protected override string ComprimirYPonerZipEnAppData()
		{
			return IODiskUtility.ComprimirUltimoBackupBdYPonerZipEnCarpetaDeBackups();
		}

		protected override string NombreDelBackupZipeadoSinExtensionNiFecha()
		{
			return "BaseDeDatos";
		}
	}
}