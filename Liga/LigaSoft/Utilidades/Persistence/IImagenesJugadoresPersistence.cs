﻿using LigaSoft.Models.ViewModels;

namespace LigaSoft.Utilidades.Persistence
{
	public interface IImagenesJugadoresPersistence
	{
		void GuardarFotoWebCam(JugadorBaseVM vm);
		void GuardarFotoDeJugadorDesdeArchivo(EditFotoJugadorDesdeArchivoVM vm);
		string GetFotoEnBase64(string dni);
		void GuardarImagenJugadorImportado(string dni, byte[] fotoByteArray);
		void Eliminar(string dni);
		string Path(string dni);
		void CambiarDNI(string dniAnterior, string dniNuevo);
	}
}