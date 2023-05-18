import { useParams } from 'react-router-dom';
import { GenericButton } from '../../common/GenericButton';

export const TablasDeLaZona = () => {
  const { torneoId, zonaId } = useParams();

  return (
    <>
      <div className='flex flex-col items-center gap-5'>
        <GenericButton
          path={`/torneo/${torneoId}/zona/${zonaId}/posiciones`}
          content='Posiciones'
        />
        <GenericButton path={`/torneo/${torneoId}/zona/${zonaId}/fixture`} content='Fixture' />
        <GenericButton path={`/torneo/${torneoId}/zona/${zonaId}/jornadas`} content='Jornadas' />
        <GenericButton path={`/torneo/${torneoId}/zona/${zonaId}/clubes`} content='Clubes' />
      </div>
    </>
  );
};
