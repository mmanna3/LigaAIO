using System;
using System.Collections.Generic;

namespace LigaSoft.Models.Otros
{
	//Atributos en minúscula porque matchea parámetros de request hecho por gijgo.grid.js
	public class GijgoGridOpciones
	{
		public int? page { get; set; }
		public int? limit { get; set; }
		public string sortBy { get; set; }
		public string direction { get; set; }
		public string searchField { get; set; }
		public string searchValue { get; set; }
		public string filterField { get; set; }
		public string filterValue { get; set; }
		public string filterOperator { get; set; }
		public IList<GijgoGridFilter> filters { get; set; }

		public GijgoGridOpciones()
		{
			filters = new List<GijgoGridFilter>();
		}
	}

	public class GijgoGridFilter
	{
		public GijgoGridFilter(string field, string @operator, string value)
		{
			this.field = field;
			this.@operator = @operator;
			this.value = value;
		}

		public GijgoGridFilter(string field, string value)
		{
			this.field = field;
			this.@operator = "=";
			this.value = $"\"{value}\"";
		}

		public GijgoGridFilter(string field, int value)
		{
			this.field = field;
			this.@operator = "=";
			this.value = value.ToString();
		}

		public GijgoGridFilter(string field, bool value)
		{
			this.field = field;
			this.@operator = "=";
			this.value = value.ToString();
		}

		public GijgoGridFilter(string field, Enum value)
		{
			this.field = field;
			this.@operator = "=";
			this.value = $"\"{value.ToString()}\"";
		}

		public string field { get; set; }
		public string @operator { get; set; }
		public string value { get; set; }
	}
}