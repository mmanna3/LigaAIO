using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.ViewModels
{
	public class JornadaVM : ViewModelConId
	{
		public int FechaId { get; set; }
		public string Fecha { get; set; }

		public string Titulo { get; set; }
		public string Subtitulo { get; set; }

		[Display(Name="Local")]
		public string Local { get; set; }
		public int LocalId { get; set; }

		[Display(Name = "Visitante")]
		public string Visitante { get; set; }
		public int VisitanteId { get; set; }

		public virtual List<PartidoVM> Partidos { get; set; }

		[Display(Name = "Resultados verificados")]
		public bool ResultadosVerificadosBool { get; set; }

		[Display(Name = "Resultados verificados")]
		public string ResultadosVerificados { get; set; }
	}
}