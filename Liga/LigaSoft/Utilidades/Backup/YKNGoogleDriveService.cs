using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;

namespace LigaSoft.Utilidades.Backup
{
	public class YKNGoogleDriveService
	{
		private static readonly string[] Scopes = { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
		private const string ApplicationName = "Edefi backup";
		private static readonly string CredencialesPath = HostingEnvironment.MapPath("~/Utilidades/Backup/Recursos/credentials.json");
		private static readonly string TokenPath = HostingEnvironment.MapPath("~/Utilidades/Backup/Recursos");
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
				throw new Exception("Google Drive: Error al intentar subir el archivo");
		}
		
		public async Task SubirArchivoAsync(string filePath, string fileName, string mimeType)
		{
			var fileMetadata = new File { Name = fileName };
			var request = _driveService.Files.Create(fileMetadata, new FileStream(filePath, FileMode.Open), mimeType);
			request.Fields = "id";
			request.ChunkSize = 256 * 1024; // Tamaño de cada fragmento

			try
			{
				await request.UploadAsync();
			}
			catch (Google.GoogleApiException ex)
			{
				// Manejar diferentes tipos de errores
				if (ex.Error.Code == 403)
				{
					throw new Exception("Acceso denegado. Verifica tus permisos.");
				}
				else if (ex.Error.Code == 404)
				{
					throw new Exception("Archivo o carpeta no encontrado.");
				}
				else
				{
					throw new Exception("Error desconocido: " + ex.Message);
				}
			}
		}

		public void DeleteFile(string fileId)
		{
			try
			{
				var request = _driveService.Files.Delete(fileId);
				request.Execute();				
			}
			catch (Exception e)
			{
				throw new Exception("Google Drive: Error al intentar eliminar archivo", e);
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
				throw new Exception("Google Drive: Error al intentar listar archivos", e);
			}
		}
	}
}