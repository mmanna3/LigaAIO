import { Link } from 'react-router-dom';
import IMG_TORNEOS_TITLE from '../../../assets/images/mobile/titles/torneos-title.avif';
import BTN_TORNEO_BABY from '../../../assets/images/mobile/buttons/btn-baby.avif';
import BTN_TORNEO_FUTSAL from '../../../assets/images/mobile/buttons/btn-futsal.avif';
import BTN_TORNEO_FUTBOL11 from '../../../assets/images/mobile/buttons/btn-futbol11.avif';
import IMG_TORNEO_FUTSAL from '../../../assets/images/mobile/img-futsal.avif';
import IMG_TORNEO_BABY from '../../../assets/images/mobile/img-baby.avif';
import IMG_TORNEO_FUTBOL11 from '../../../assets/images/mobile/img-futbol11.avif';
import { Title } from '../../common/Title';
import { ImageBtn } from '../../common/ImageBtn';

export const TorneosPage = () => {
  return (
    <>
      <Title alt='Torneos' img={IMG_TORNEOS_TITLE} />
      <div className='flex flex-col items-center gap-6  xl:p-10 lg:justify-center xl:flex-row '>
        <div
          key='futsal'
          className='flex items-center space-x-2 px-[10%] xl:flex-col-reverse xl:px-0'
        >
          <ImageBtn img={BTN_TORNEO_FUTSAL} alt='Futsal' url='/torneos/futsal' style='xl:p-14' />
          <ImageBtn img={IMG_TORNEO_FUTSAL} alt='Futsal-image' url='/torneos/futsal' />
        </div>
        <div
          key='baby'
          className='flex items-center space-x-2 px-[10%]  xl:flex-col-reverse xl:px-0'
        >
          <ImageBtn img={BTN_TORNEO_BABY} alt='baby-image' url='/torneos/baby' style='xl:p-14' />
          <ImageBtn img={IMG_TORNEO_BABY} alt='baby' url='/torneos/baby'  />
        </div>
        <div
          key='futbol-11'
          className='flex items-center space-x-2 px-[10%]  xl:flex-col-reverse xl:px-0'
          
        >
          <ImageBtn img={BTN_TORNEO_FUTBOL11} alt='futbol 11 image' url='/torneos/futbol -11' style='xl:p-14' />
          <ImageBtn img={IMG_TORNEO_FUTBOL11} alt='futbol 11' url='/torneos/futbol-11'  />
        </div>
      </div>
    </>
  );
};
