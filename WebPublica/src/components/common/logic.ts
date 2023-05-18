import { Torneo } from '../../interfaces/api';

const tiposDeTorneo = {
  baby: ['MATUTINO', 'VESPERTINO'],
  futsal: ['FUTSAL'],
  futbol11: ['FUTBOL 11'],
  copaEdefi: ['COPA EDEFI'],
  torneoDeVerano: ['TORNEO DE VERANO'],
  copaDeLaLiga: ['COPA DE LA LIGA'],
};

export const filterTorneosByType = (
  torneos: Torneo[],
  tipo: keyof typeof tiposDeTorneo,
): Torneo[] => {
  const torneosByType: Torneo[] = [];

  torneos.forEach((torneo) => {
    tiposDeTorneo[tipo].forEach((tipoDeTorneo) => {
      if (torneo.descripcion.toUpperCase().includes(tipoDeTorneo)) torneosByType.push(torneo);
    });
  });

  return torneosByType;
};
