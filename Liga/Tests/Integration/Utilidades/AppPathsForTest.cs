﻿using System.IO;
using System.Reflection;
using LigaSoft.Utilidades;

namespace Tests.Integration.Utilidades
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
			return GetAbsolutePath($"/Backup/{fileNameWithExtension}");
		}

		public override string BackupAbsolute()
		{
			return GetAbsolutePath("/Backup");
		}
	}
}