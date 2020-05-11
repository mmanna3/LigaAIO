import React, { useEffect } from 'react';
import Banner from './Banner/Banner';
import Footer from './Footer/Footer';
import SeccionPrincipalTorneos from 'Components/SeccionPrincipalTorneos/SeccionPrincipalTorneos';
import SeccionPrincipalNosotros from 'Components/SeccionPrincipalNosotros/SeccionPrincipalNosotros';
import SeccionPrincipalNoticias from 'Components/SeccionPrincipalNoticias/SeccionPrincipalNoticias';
import NoticiaSeleccionada from 'Components/SeccionPrincipalNoticias/NoticiaSeleccionada/NoticiaSeleccionada';
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
  const noticiaSeleccionada = useSelector(state => state.noticiaReducer.noticia);

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
    else if (seccionPrincipalSeleccionada == "Noticias")
      return <SeccionPrincipalNoticias />
    else if (seccionPrincipalSeleccionada == "Nosotros")
      return <SeccionPrincipalNosotros />    
    else if (seccionPrincipalSeleccionada == "NoticiaSeleccionada" && noticiaSeleccionada != null)
      return <NoticiaSeleccionada /> 
}

export default Home;
