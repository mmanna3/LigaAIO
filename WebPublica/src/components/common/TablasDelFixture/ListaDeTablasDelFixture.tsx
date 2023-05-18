import { useParams } from 'react-router-dom';
import { useFetch } from '../hooks/useFetch';
import { FechaDelFixture, FixtureDelTorneo } from '../../../interfaces/api';
import { Spinner } from '../Spinner';
import { RowContent, Table, TableRow } from '../Table';

export const ListaDeTablasDelFixture = () => {
  const { zonaId } = useParams();
  const { data, isFetching } = useFetch<FixtureDelTorneo>(`fixture?zonaId=${zonaId}`);

  if (isFetching) {
    return <Spinner />;
  }

  const fechas: FechaDelFixture[] = [...data.Fechas];

  return (
    <>
      {fechas.map(({ Titulo, DiaDeLaFecha, LocalVisitante }) => (
        <>
          <tr className=' flex justify-between bg-[#101010] text-xl text-white'>
          <RowContent content={Titulo}/>
          <RowContent content={DiaDeLaFecha}/>
          </tr>
          <Table key={DiaDeLaFecha}>
            <tbody>
              {LocalVisitante.map(({ Local, Visitante, EscudoLocal, EscudoVisitante }, i) => (
                <TableRow key={i}>
                  <RowContent content={EscudoLocal} type='Img' />
                  <RowContent content={Local} />
                  <RowContent content={'vs.'} />
                  <RowContent content={Visitante} />
                  <RowContent content={EscudoVisitante} type='Img' />
                </TableRow>
              ))}
            </tbody>
          </Table>
        </>
      ))}
    </>
  );
};
