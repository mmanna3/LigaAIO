using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;

namespace LigaSoft.Builders
{
    public class ZonaHelper
    {
	    private readonly ApplicationDbContext _context;

		public ZonaHelper(ApplicationDbContext context)
		{
			_context = context;
		}

	    public List<SelectListItem> EquiposDeLaZona(Zona zona)
	    {
		    switch (zona.Torneo.Tipo.Formato)
		    {
			    case TorneoFormato.AperturaClausura:
				    return EquiposDeLaZonaAperturaClausura(zona);
			    case TorneoFormato.Relampago:
				    return EquiposDeLaZonaRelampago(zona);
			    default:
				    throw new ArgumentOutOfRangeException();
		    }
	    }

	    public List<Equipo> EquiposDeLaZonaDatosParaLosDatosWebPublica(Zona zona)
	    {
		    switch (zona.Torneo.Tipo.Formato)
		    {
			    case TorneoFormato.AperturaClausura:
				    return EquiposDeLaZonaDatosParaLosDatosWebPublicaAperturaClausura(zona);
			    case TorneoFormato.Relampago:
				    return EquiposDeLaZonaDatosParaLosDatosWebPublicaAperturaRelampago(zona);
			    default:
				    throw new ArgumentOutOfRangeException();
		    }
	    }

	    private static List<Equipo> EquiposDeLaZonaDatosParaLosDatosWebPublicaAperturaClausura(Zona zona)
	    {
		    if (!zona.Fechas.Any())
			    return zona.Equipos.ToList();

			var locales = zona.Fechas.SelectMany(x => x.Jornadas).Select(y => y.Local).Where(x => x != null);
			var visitantes = zona.Fechas.SelectMany(x => x.Jornadas).Select(y => y.Visitante).Where(x => x != null);

			return locales.Concat(visitantes).Distinct().ToList();			
	    }

	    private List<Equipo> EquiposDeLaZonaDatosParaLosDatosWebPublicaAperturaRelampago(Zona zona)
	    {
		    return _context.ZonaRelampagoEquipos.Where(x => x.Zona.Id == zona.Id).Select(y => y.Equipo).Where(x => x != null).ToList();
	    }

	    public List<TextValueItem> EquiposDelTorneoSinZona(Zona zona)
	    {
		    switch (zona.Torneo.Tipo.Formato)
		    {
			    case TorneoFormato.AperturaClausura:
				    return EquiposDelTorneoSinZonaAperturaClausura(zona);
				case TorneoFormato.Relampago:
				    return EquiposDelTorneoSinZonaRelampago(zona);
				default:
				    throw new ArgumentOutOfRangeException();
		    }
		}

		private List<SelectListItem> EquiposDeLaZonaRelampago(Zona zona)
	    {
		    return _context.ZonaRelampagoEquipos
			    .Where(x => x.Zona.Id == zona.Id)
				.ToList()
				.Select(x => new SelectListItem { Text = $"{x.Equipo.Id} - {x.Equipo.Nombre}", Value = x.Equipo.Id.ToString() })
			    .ToList();
	    }

	    private static List<SelectListItem> EquiposDeLaZonaAperturaClausura(Zona zona)
	    {
		    return zona?.Equipos
			    .ToList()
				.Select(x => new SelectListItem { Text = $"{x.Id} - {x.Nombre}", Value = x.Id.ToString() })
			    .ToList();
	    }

	    private List<TextValueItem> EquiposDelTorneoSinZonaAperturaClausura(Zona zona)
	    {
		    return _context.Equipos
			    .Where(x => x.Torneo.Id == zona.TorneoId && (x.Zona == null || x.Zona.Tipo != zona.Tipo))
			    .ToList()
				.Select(x => new TextValueItem { Text = $"{x.Id} - {x.Nombre}", Value = x.Id.ToString() })
			    .ToList();
	    }

	    private List<TextValueItem> EquiposDelTorneoSinZonaRelampago(Zona zona)
	    {
		    var equiposQueEstanEnLaZona = _context.ZonaRelampagoEquipos.Where(x => x.Zona.Id == zona.Id).Select(y => y.Equipo.Id);

		    return _context.Equipos
			    .Where(x => !equiposQueEstanEnLaZona.Contains(x.Id))
				.ToList()
				.Select(x => new TextValueItem { Text = $"{x.Id} - {x.Nombre}", Value = x.Id.ToString() })
			    .ToList();
	    }

	    public Zona ZonaClausura(Zona zonaApertura)
	    {
		    if (!zonaApertura.Tipo.Equals(ZonaTipo.Apertura))
			    return null;

		    return _context.Zonas.SingleOrDefault(x => x.TorneoId == zonaApertura.TorneoId && x.Nombre == zonaApertura.Nombre && x.Tipo == ZonaTipo.Clausura);
	    }

	}
}