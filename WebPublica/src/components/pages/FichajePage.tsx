import { Title } from '../common/Title'
import SeccionPrincipalFichaje from './SeccionPrincipalFichaje/SeccionPrincipalFichaje';
import IMG_TITLE_FICHAJE from '../../assets/images/mobile/titles/fichaje-title.avif'
export const FichajePage = () => {
  return (
    <>
    <Title img={IMG_TITLE_FICHAJE} alt='Fichaje' />
      <SeccionPrincipalFichaje />;
    </>
  );
};
