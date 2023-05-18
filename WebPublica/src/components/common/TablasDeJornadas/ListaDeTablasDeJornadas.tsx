import { useParams } from 'react-router-dom';
import { useFetch } from '../hooks/useFetch';
import { Spinner } from '../Spinner';
import { RowContent, Table, TableRow } from '../Table';
import { JornadasDelTorneo } from '../../../interfaces/api';

export const ListaDeTablasDeJornadas = () => {
  const { zonaId } = useParams();
  const { data, isFetching } = useFetch(`jornadas?zonaId=${zonaId}`);

  const jornadas: JornadasDelTorneo = data;

  if (isFetching) {
    return <Spinner />;
  }
  return (
    <div className='mx-auto grid gap-2 sm:grid-cols-2'>
      {jornadas.JornadasPorFecha.map(({ FechaId, FechaNumero, Renglones }) => (
        <Table key={FechaId} titulo={`${FechaNumero}`}>
          <thead>
            <TableRow type={'tableHead'}>
              <RowContent content={''} />
              <RowContent content={'Esc'} />
              <RowContent content={'Equipo'} />
              <RowContent content={'1ra'} />
              <RowContent content={'Rva.'} />
              <RowContent content={'3ra'} />
              <RowContent content={'4ta'} />
              <RowContent content={'5ta'} />
              <RowContent content={'T.P.'} />
              <RowContent content={'P.J.'} />
              <RowContent content={'V'} />
            </TableRow>
          </thead>
          <tbody>
            {Renglones.map(
              (
                {
                  JornadaId,
                  JornadaNumero,
                  Escudo,
                  Equipo,
                  ResultadosPorCategorias,
                  PuntosTotales,
                  PartidosJugados,
                  PartidoVerificado,
                },
                i,
              ) => (
                <TableRow key={JornadaId * i}>
                  <RowContent content={JornadaNumero} />
                  <RowContent type={'Img'} content={Escudo} />
                  <RowContent content={Equipo} />
                  {ResultadosPorCategorias.map(({ Goles, Orden }, i) => (
                    <RowContent key={Orden * i} content={Goles} />
                  ))}
                  <RowContent content={PuntosTotales} />
                  <RowContent content={PartidosJugados} />
                  <RowContent content={PartidoVerificado} />
                </TableRow>
              ),
            )}
          </tbody>
        </Table>
      ))}
    </div>
  );
};
