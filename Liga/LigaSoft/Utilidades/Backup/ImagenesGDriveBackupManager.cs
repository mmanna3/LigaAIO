using System;
using System.IO;
using System.Linq;

namespace LigaSoft.Utilidades.Backup
{
	public class ImagenesGDriveBackupManager : GDriveBackupManager
	{
		protected override string ComprimirYPonerZipEnAppData()
		{
			return IODiskUtility.ComprimirImagenesYPonerZipEnAppData();
		}

		protected override string NombreDelBackupZipeadoSinExtensionNiFecha()
		{
			return "Imagenes";
		}
	}
}