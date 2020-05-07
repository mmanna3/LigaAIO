import React from 'react';
import BannerHome from './BannerHome/BannerHome';
import Footer from './Footer/Footer';
import SeccionPrincipalTorneos from 'Components/SeccionPrincipalTorneos/SeccionPrincipalTorneos';
import {COLOR} from "Utils/consts";
import {useSelector} from 'react-redux';
import styles from './Home.css';


function Home() {
    const seccionPrincipalSeleccionada = useSelector(state => state.seccionPrincipalReducer.seccionPrincipal);

    if (!seccionPrincipalSeleccionada)
      return (
        <div className={styles.home}>
          <div className={styles.banners}>
            <BannerHome titulo="Torneos" color={COLOR.ROJO} />
            <BannerHome titulo="Noticias" color={COLOR.VERDE} />
            <BannerHome titulo="Nosotros" color={COLOR.AZUL} />          
          </div>
          <Footer/>
        </div>
      )
    else if (seccionPrincipalSeleccionada == "Torneos")
      return <SeccionPrincipalTorneos />
    return null;
}

export default Home;
