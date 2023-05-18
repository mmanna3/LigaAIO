export interface Torneo {
  descripcion: string;
  id: string;
  formato: string;
}

export interface Zona {
  descripcion: string;
  zonaAperturaId: number;
  zonaClausuraId: number;
  zonaRelampagoId?: number;
}

export interface Tabla {
  CategoriaId: number;
  Categoria: string;
  Renglones: Renglon[];
}

export interface Renglon {
  Df: number;
  Equipo: string;
  EquipoId: number;
  Escudo: string;
  Gc: number;
  Gf: number;
  Np: number;
  Pe: number;
  Pg: number;
  Pj: number;
  Posicion: number;
  Pp: number;
  Pts: number;
}

/* POSICIONES */
export interface PosicionesDelTorneo {
  ZonaId: number;
  TorneoId: number;
  Titulo: string;
  VerGoles: boolean;
  TablasPorCategoria: Tabla[];
  TablaGeneral: Tabla;
}

/* FIXTURE */
export interface FixtureDelTorneo {
  ZonaId: number;
  TorneoId: number;
  Titulo: string;
  Fechas: FechaDelFixture[];
  PublicadoBool: boolean;
}

export interface FechaDelFixture {
  Titulo: string;
  DiaDeLaFecha: string;
  LocalVisitante: DatosDeAmbosEquipos[];
  EquipoLibre?: null;
}

export interface DatosDeAmbosEquipos {
  LocalId: number;
  Local: string;
  EscudoLocal: string;
  VisitanteId: number;
  Visitante: string;
  EscudoVisitante: string;
}

/* JORNADAS */
export interface Resultado {
  Orden: number;
  Goles: string;
}

export interface Jornada {
  JornadaId: number;
  JornadaNumero: number;
  Escudo: string;
  EquipoId: number;
  Equipo: string;
  ResultadosPorCategorias: Resultado[];
  PuntosTotales: number;
  PartidosJugados: number;
  PartidoVerificado: string;
}

export interface Categoria {
  Nombre: string;
  Orden: number;
  TorneoId: number;
  Torneo?: number;
  Id: number;
}

export interface JornadasDelTorneo {
  Titulo: string;
  JornadasPorFecha: JornadaPorFecha[];
  Categorias: Categoria[];
  JornadasVerificadasId?: number;
  TorneoId: number;
  ZonaId: number;
}

export interface JornadaPorFecha {
  FechaId: number;
  FechaNumero: number;
  Renglones: Jornada[];
  Publicada: string;
  PublicadaBool: boolean;
}

/* CLUBES */
export interface Clubes {
  Titulo?: string;
  Renglones: Club[];
  TorneoId?: number;
  ZonaId?: number;
}
export interface Club {
  Equipo: string;
  Escudo: string;
  Localidad: string;
  Direccion: string;
  Techo: number;
  TechoDescripcion: string;
  Delegado1?: number;
  Delegado2?: number;
  Telefono1?: number;
  Telefono2?: number;
}


/* NOTICIAS */
export interface Noticia {
  id: number;
  titulo: string;
  subtitulo: string;
  fecha: string;
  cuerpo?: string;
}
