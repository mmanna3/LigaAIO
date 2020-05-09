import React, { useEffect } from 'react';
import Banner from './Banner/Banner';
import Footer from './Footer/Footer';
import SeccionPrincipalTorneos from 'Components/SeccionPrincipalTorneos/SeccionPrincipalTorneos';
import {COLOR} from "Utils/consts";
import {useSelector} from 'react-redux';
import styles from './Home.css';


function Home() {
  
  useEffect(() => {
    
    inicializarVariableWindowInnerHeight();

    window.addEventListener('resize', () => {
      inicializarVariableWindowInnerHeight()
    });

    function inicializarVariableWindowInnerHeight() {
      
      let windowInnerHeight = window.innerHeight;
      
      if (window.innerWidth >= 576) { //Es Desktop
        if (windowInnerHeight < 400)
          windowInnerHeight = 600;
      } else {  //Es mobile
        if (windowInnerHeight < 500)
          windowInnerHeight = 500;
      }
      
      document.documentElement.style.setProperty('--windowInnerHeight', `${windowInnerHeight}px`);
    }

  }, []);

  const seccionPrincipalSeleccionada = useSelector(state => state.seccionPrincipalReducer.seccionPrincipal);

    if (!seccionPrincipalSeleccionada)
      return (
        <div className={styles.home}>
          <div className={styles.banners}>
            <Banner titulo="Torneos" color={COLOR.ROJO} />
            <Banner titulo="Noticias" color={COLOR.VERDE} />
            <Banner titulo="Nosotros" color={COLOR.AZUL} />          
          </div>
          <Footer/>
        </div>
      )
    else if (seccionPrincipalSeleccionada == "Torneos")
      return <SeccionPrincipalTorneos />
    return null;
}

export default Home;
