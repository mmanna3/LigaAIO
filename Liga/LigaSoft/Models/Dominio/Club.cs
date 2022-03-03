using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LigaSoft.Models.Dominio.Finanzas;
using LigaSoft.Models.Enums;
using LigaSoft.Models.Interfaces;
using LigaSoft.Utilidades;

namespace LigaSoft.Models.Dominio
{
	public class Club : IClassConIdDescripcion
	{
		public int Id { get; set; }

		[NotMapped]
		public string Descripcion
		{
			get => Nombre;
			set => throw new System.NotImplementedException();
		}

		[Required]
		public string Nombre { get; set; }

		public string Localidad { get; set; }

		public string Direccion { get; set; }

		public bool? Techo { get; set; }

		public virtual ICollection<Equipo> Equipos { get; set; }
		public virtual ICollection<Delegado> Delegados { get; set; }
		public virtual ICollection<MovimientoEntradaConClub> Movimientos { get; set; }
		
		public Techo TechoBoolToTechoEnum()
		{
			switch (Techo)
			{
				case true:
					return Enums.Techo.Si;
				case false:
					return Enums.Techo.No;
				default:
					return Enums.Techo.Indeterminado;
			}			
		}

		public int Cuota()
		{
			return Equipos.Sum(x => x.ValorDeLaCuota);
		}

		public int DeudaTotal()
		{
			return Movimientos.Sum(x => x.ImporteAdeudado());
		}

		public int DeudaFichajes()
		{
			return Movimientos.Where(x => x.Concepto.Id == (int)ConceptoTipoEnum.Fichaje).Sum(x => x.ImporteAdeudado());
		}

		public int DeudaCuotas()
		{
			return Movimientos.Where(x => x.Concepto.Id == (int)ConceptoTipoEnum.Cuota).Sum(x => x.ImporteAdeudado());
		}

		public IEnumerable<Equipo> EquiposActivos()
		{
			return Equipos.Where(x => !x.BajaLogica);
		}

		public int DeudaLibre()
		{
			return Movimientos.Where(x => x.Concepto.Id == (int)ConceptoTipoEnum.Libre).Sum(x => x.ImporteAdeudado());
		}

		public int DeudaInsumos()
		{
			return Movimientos.Where(x => x.Concepto.Id > 3).Sum(x => x.ImporteAdeudado());
		}
	}

}