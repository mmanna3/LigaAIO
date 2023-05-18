import { useParams } from 'react-router-dom';
import { useFetch } from '../hooks/useFetch';
import { Spinner } from '../Spinner';
import { RowContent, Table, TableRow } from '../Table';
import { PosicionesDelTorneo, Renglon, Tabla } from '../../../interfaces/api';

/* //ESTE COMPONENTE SE VA A ENCARGAR DE FILTRAR LA DATA Y PASARSELA A LA TABLA GENERICA. */

export const ListaDeTablasDePosiciones = () => {
  const { zonaId } = useParams();
  const { data, isFetching } = useFetch(`/posiciones?zonaId=${zonaId}`);
  const { TablaGeneral, TablasPorCategoria }: PosicionesDelTorneo = data;

  if (isFetching) {
    return <Spinner />;
  }

  return (
    <div className='mx-auto grid gap-2 sm:grid-cols-2'>
      {TablasPorCategoria.map(({ CategoriaId, Categoria, Renglones }: Tabla) => (
        <Table key={CategoriaId} titulo={Categoria}>
          <thead>
            <TableRow type={'tableHead'}>
              <RowContent content={'Pos'} />
              <RowContent content={'Esc'} />
              <RowContent content={'Equipo'} />
              <RowContent content={'J'} />
              <RowContent content={'G'} />
              <RowContent content={'E'} />
              <RowContent content={'P'} />
              <RowContent content={'Np'} />
              <RowContent content={'Pts'} />
            </TableRow>
          </thead>
          <tbody>
            {Renglones.map(({ EquipoId, Posicion, Escudo, Equipo, Pj, Pg, Pe, Pp, Np, Pts }) => (
              <TableRow key={EquipoId}>
                <RowContent content={Posicion} />
                <RowContent type={'Img'} content={Escudo} />
                <RowContent content={Equipo} />
                <RowContent content={Pj} />
                <RowContent content={Pg} />
                <RowContent content={Pe} />
                <RowContent content={Pp} />
                <RowContent content={Np} />
                <RowContent content={Pts} />
              </TableRow>
            ))}
          </tbody>
        </Table>
      ))}

      <Table key={TablaGeneral.CategoriaId} titulo={'General'}>
        <thead>
          <TableRow type={'tableHead'}>
            <RowContent content={'Pos'} />
            <RowContent content={'Esc'} />
            <RowContent content={'Equipo'} />
            <RowContent content={'J'} />
            <RowContent content={'G'} />
            <RowContent content={'E'} />
            <RowContent content={'P'} />
            <RowContent content={'Np'} />
            <RowContent content={'Pts'} />
          </TableRow>
        </thead>
        <tbody>
          {TablaGeneral.Renglones.map(
            ({ EquipoId, Posicion, Escudo, Equipo, Pj, Pg, Pe, Pp, Np, Pts }: Renglon) => (
              <TableRow key={EquipoId}>
                <RowContent content={Posicion} />
                <RowContent type={'Img'} content={Escudo} />
                <RowContent content={Equipo} />
                <RowContent content={Pj} />
                <RowContent content={Pg} />
                <RowContent content={Pe} />
                <RowContent content={Pp} />
                <RowContent content={Np} />
                <RowContent content={Pts} />
              </TableRow>
            ),
          )}
        </tbody>
      </Table>
    </div>
  );
};
