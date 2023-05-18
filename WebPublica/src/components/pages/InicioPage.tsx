import { ImageBtn } from '../common/ImageBtn';
import BANNER_CELU from '../../assets/images/mobile/banner-celu.avif';
import BANNER_DESKTOP from '../../assets/images/desktop/banner-desktop.avif';
import IMG_BTN_TORNEOS from '../../assets/images/mobile/buttons/torneos.avif';
import IMG_BTN_COPAS from '../../assets/images/mobile/buttons/copas.avif';
import IMG_BTN_FICHAJE from '../../assets/images/mobile/buttons/fichaje.avif';
import IMG_BTN_NOSOTROS from '../../assets/images/mobile/buttons/nosotros.avif';
import IMG_BTN_CONTACTO from '../../assets/images/mobile/buttons/contacto.avif';

export const InicioPage = () => {
  return (
    <>
      <img className='mx-auto sm:hidden w-[90%] h-[130px]' src={BANNER_CELU} alt='banner-image' />
      <img className='m-auto hidden sm:block w-[90%] h-[180px] ' src={BANNER_DESKTOP} alt='banner-image' />
      <div className=' mx-auto flex flex-col items-center gap-6 p-16 xs:max-w-sm md:max-w-md xl:max-w-xl'>
        <ImageBtn img={IMG_BTN_TORNEOS} alt='torneos' url='/torneos' />
        <ImageBtn img={IMG_BTN_COPAS} alt='copas' url='/copas' />
        <ImageBtn img={IMG_BTN_NOSOTROS} alt='nosotros' url='/nosotros' />
        <ImageBtn img={IMG_BTN_CONTACTO} alt='contacto' url='/contacto' />
        <ImageBtn img={IMG_BTN_FICHAJE} alt='contacto' url='/contacto' />
      </div>
    </>
  );
};
