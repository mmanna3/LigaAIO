﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web.Hosting;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;

namespace LigaSoft.Utilidades.Backup
{
	public class YKNGoogleDriveService
	{
		private static readonly string[] Scopes = { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
		private const string ApplicationName = "Edefi backup";
		private static readonly string CredencialesPath = HostingEnvironment.MapPath("~/Utilidades/Backup/credentials.json");
		private static readonly string TokenPath = HostingEnvironment.MapPath("~/Utilidades/Backup/token.json");
		private static DriveService _driveService;		

		public YKNGoogleDriveService()
		{
			_driveService = GetServiceOAuth();
		}

		public static DriveService GetServiceOAuth()
		{
			UserCredential credential;

			using (var stream =
				new FileStream(CredencialesPath, FileMode.Open, FileAccess.Read))
			{
				// The file token.json stores the user's access and refresh tokens, and is created
				// automatically when the authorization flow completes for the first time.
				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.Load(stream).Secrets,
					Scopes,
					"user",
					CancellationToken.None,
					new FileDataStore(TokenPath, true)).Result;
				Console.WriteLine("Credential file saved to: " + TokenPath);
			}

			var service = new DriveService(new BaseClientService.Initializer
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName,
			});

			return service;
		}

		public void SubirArchivo(string path, string fileName, string contentType)
		{
			var fileMetadata = new File { Name = fileName };			

			FilesResource.CreateMediaUpload request;
			using (var stream = new FileStream(path, FileMode.Open))
			{
				request = _driveService.Files.Create(fileMetadata, stream, contentType);
				request.Fields = "id";
				request.Upload();
			}
			var file = request.ResponseBody;

			if (file.Id == null)
				throw new Exception("Error al subir el archivo");
		}

		public void DeleteFile(string fileId)
		{
			try
			{
				var request = _driveService.Files.Delete(fileId);
				request.Execute(); //Los logs de este método no sirven. Necesito darme cuenta si pudo eliminar o no.
				Log.Info($"Archivo de Drive eliminado con éxito. Id del archivo: {fileId}");
			}
			catch (Exception e)
			{
				Log.Error("Error al intentar eliminar archivo de Drive: " + e.Message);
			}
		}

		public IList<File> ListAll() //Los logs de este método no sirven. Necesito darme cuenta si pudo listar o no.
		{
			try
			{
				var listRequest = _driveService.Files.List();
				listRequest.PageSize = 100;
				listRequest.Fields = "nextPageToken, files(id, name, createdTime)";

				return listRequest.Execute().Files;
			}
			catch (Exception e)
			{
				throw new Exception("Error al intentar listar archivos: ", e);
			}
		}
	}
}