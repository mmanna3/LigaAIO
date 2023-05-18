import { describe, it } from 'vitest';
import { filterTorneosByType } from './logic';
import { Torneo } from '../../interfaces/api';

describe('filterTorneosByType', () => {
  it('Filter "COPA EDEFI" correctly', ({ expect }) => {
    // Arrange
    const torneos: Torneo[] = [
      { descripcion: 'COPA EDEFI Vespertino', id: '1', formato: 'relampago' },
      { descripcion: 'COPA EDEFI Matutino', id: '3', formato: 'relampago' },
      { descripcion: 'COPA DE LA LIGA Bla bla bla', id: '2', formato: 'relampago' },
      { descripcion: 'TORNEO DE VERANO 5 CAT', id: '4', formato: 'relampago' },
    ];

    // Act
    const resultado = filterTorneosByType(torneos, 'copaEdefi');

    // Assert
    expect(resultado.length).toBe(2);
    expect(resultado[0].descripcion).toBe('COPA EDEFI Vespertino');
    expect(resultado[1].descripcion).toBe('COPA EDEFI Matutino');
  });

  it('Filter "TORNEO DE VERANO" correctly', ({ expect }) => {
    // Arrange
    const torneos: Torneo[] = [
      { descripcion: 'COPA EDEFI Vespertino', id: '1', formato: 'relampago' },
      { descripcion: 'COPA EDEFI Matutino', id: '3', formato: 'relampago' },
      { descripcion: 'COPA DE LA LIGA Bla bla bla', id: '2', formato: 'relampago' },
      { descripcion: 'TORNEO DE VERANO 5 CAT', id: '4', formato: 'relampago' },
    ];

    // Act
    const resultado = filterTorneosByType(torneos, 'torneoDeVerano');

    // Assert
    expect(resultado.length).toBe(1);
    expect(resultado[0].descripcion).toBe('TORNEO DE VERANO 5 CAT');
  });

  it('Filter "COPA DE LA LIGA" correctly', ({ expect }) => {
    // Arrange
    const torneos: Torneo[] = [
      { descripcion: 'COPA EDEFI Vespertino', id: '1', formato: 'relampago' },
      { descripcion: 'COPA EDEFI Matutino', id: '3', formato: 'relampago' },
      { descripcion: 'COPA DE LA LIGA Bla bla bla', id: '2', formato: 'relampago' },
      { descripcion: 'TORNEO DE VERANO 5 CAT', id: '4', formato: 'relampago' },
    ];

    // Act
    const resultado = filterTorneosByType(torneos, 'copaDeLaLiga');

    // Assert
    expect(resultado.length).toBe(1);
    expect(resultado[0].descripcion).toBe('COPA DE LA LIGA Bla bla bla');
  });

  it('Filter "futsal" correctly', ({ expect }) => {
    // Arrange
    const torneos: Torneo[] = [
      { descripcion: 'MATUTINO 6 CATEGORÍAS', id: '1', formato: 'aperturaClausura' },
      { descripcion: 'FUTSAL MAYORES', id: '3', formato: 'aperturaClausura' },
      { descripcion: 'MATUTINO 5 CATEGORÍAS', id: '2', formato: 'aperturaClausura' },
    ];

    // Act
    const resultado = filterTorneosByType(torneos, 'futsal');

    // Assert
    expect(resultado.length).toBe(1);
    expect(resultado[0].descripcion).toBe('FUTSAL MAYORES');
  });

  it('Filter "baby" correctly', ({ expect }) => {
    // Arrange
    const torneos: Torneo[] = [
      { descripcion: 'MATUTINO 6 CATEGORÍAS', id: '1', formato: 'aperturaClausura' },
      { descripcion: 'FUTSAL MAYORES', id: '3', formato: 'aperturaClausura' },
      { descripcion: 'MATUTINO 5 CATEGORÍAS', id: '2', formato: 'aperturaClausura' },
    ];

    // Act
    const resultado = filterTorneosByType(torneos, 'baby');

    // Assert
    expect(resultado.length).toBe(2);
    expect(resultado[0].descripcion).toContain('MATUTINO');
    expect(resultado[1].descripcion).toContain('MATUTINO');
  });

  it('Filter "futbol11" correctly', ({ expect }) => {
    // Arrange
    const torneos: Torneo[] = [
      { descripcion: 'MATUTINO 6 CATEGORÍAS', id: '1', formato: 'aperturaClausura' },
      { descripcion: 'FUTSAL MAYORES', id: '3', formato: 'aperturaClausura' },
      { descripcion: 'MATUTINO 5 CATEGORÍAS', id: '2', formato: 'aperturaClausura' },
    ];

    // Act
    const resultado = filterTorneosByType(torneos, 'futbol11');

    // Assert
    expect(resultado.length).toBe(0);
  });
});
