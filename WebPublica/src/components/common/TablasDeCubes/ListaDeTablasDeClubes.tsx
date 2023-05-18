import { useParams } from 'react-router-dom';
import { useFetch } from '../hooks/useFetch';
import { Clubes, Renglon } from '../../../interfaces/api';
import { Spinner } from '../Spinner';
import { TablaDeClubes } from './TablaDeClubes';
import { RowContent, Table, TableRow } from '../Table';

export const ListaDeTablasDeClubes = () => {
  const { zonaId } = useParams();
  const { data, isFetching } = useFetch<Clubes>(`/clubes?zonaId=${zonaId}`);

  const { Renglones }: Clubes = data;

  if (isFetching) {
    return <Spinner />;
  }

  return (
    <div className='mx-auto grid gap-2 sm:grid-cols-1'>
      <Table>
        <thead>
          <TableRow type={'tableHead'}>
            <RowContent content={'Equipo'} />
            <RowContent content={'Localidad'} />
            <RowContent content={'Dirección'} />
            <RowContent content={'Techo'} />
          </TableRow>
        </thead>
        <tbody>
          {Renglones.map(({ Equipo, Localidad, Direccion, TechoDescripcion, Escudo }) => (
            <TableRow key={Equipo}>
              <RowContent type={'Img'} content={Escudo} />
              <RowContent content={Localidad} />
              <RowContent content={Direccion} />
              <RowContent content={TechoDescripcion} />
            </TableRow>
          ))}
        </tbody>
      </Table>
    </div>
  );
};
