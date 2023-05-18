import IMG_COPAS_TITLE from '../../../assets/images/mobile/titles/copas-title.avif';
import IMG_COPA_EDEFI from '../../../assets/images/mobile/buttons/copa-edefi.avif';
import IMG_COPA_LIGA from '../../../assets/images/mobile/buttons/copa-liga.avif';
import IMG_TORNEO_VERANO from '../../../assets/images/mobile/buttons/torneo-verano.avif';
import { Title } from '../../common/Title';
import { ImageBtn } from '../../common/ImageBtn';

export const CopasPage = () => {
  return (
    <>
      <Title img={IMG_COPAS_TITLE} alt='Copas' />
      <div className='mx-auto flex flex-col items-center gap-8 xl:gap-20 xl:flex-row xl:justify-center px-16 xl:p-4 xl:max-w-[1500px] max-w-md'>
        <ImageBtn img={IMG_COPA_EDEFI} alt='Copa Edefi' url='/copas/copaedefi' />
        <ImageBtn img={IMG_TORNEO_VERANO} alt='Torneos de verano' url='/copas/torneoverano' />
        <ImageBtn img={IMG_COPA_LIGA} alt='Copas de la liga' url='/copas/delaliga' />
      </div>
    </>
  );
};
