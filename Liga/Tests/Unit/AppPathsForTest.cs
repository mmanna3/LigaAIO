using System.IO;
using System.Reflection;
using LigaSoft.Utilidades;

namespace Tests.Unit
{
	internal class AppPathsForTest : AppPaths
	{
		protected override string GetAbsolutePath(string relativePath)
		{
			var asemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			return $"{asemblyPath}{relativePath}";
		}

		public override string BackupAbsoluteOf(string fileNameWithExtension)
		{
			throw new System.NotImplementedException();
		}

		public override string BackupAbsolute()
		{
			throw new System.NotImplementedException();
		}
	}
}