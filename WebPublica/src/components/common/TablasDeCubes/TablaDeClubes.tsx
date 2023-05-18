type TablaDeClubesProps = {
  equipo: string;
  localidad: string;
  direccion: string;
  techo: string;
};

export const TablaDeClubes = ({ equipo, localidad, direccion, techo }: TablaDeClubesProps) => {
  return (
    <div className='m-2'>
      <table className='grid table-auto bg-white'>
        <thead>
          <tr className='grid grid-cols-4 text-center'>
            <th>Equipo</th>
            <th>Localidad</th>
            <th>Dirección</th>
            <th>Techo</th>
          </tr>
        </thead>

        <tbody>
          <tr className='grid grid-cols-4 text-center'>
            <td>{equipo}</td>
            <td>{localidad}</td>
            <td>{direccion}</td>
            <td>{techo}</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};
