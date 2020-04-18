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
		public GijgoGridFilter[] filters { get; set; }
	}

	public class GijgoGridFilter
	{
		public GijgoGridFilter()
		{
		}

		public GijgoGridFilter(string field, string @operator, int value)
		{
			this.field = field;
			this.@operator = @operator;
			this.value = value.ToString();
		}

		public GijgoGridFilter(string field, object value)
		{
			this.field = field;
			this.@operator = "=";

			if (value is int || value is bool)
				this.value = $"{value.ToString()}";
			else
				this.value = $"\"{value.ToString()}\"";
		}

		public string field { get; set; }
		public string @operator { get; set; }
		public string value { get; set; }
	}
}