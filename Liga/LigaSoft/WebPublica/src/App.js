import React from 'react';
import BannerHome from 'Components/BannerHome/BannerHome.js';
import SeccionPrincipalTorneos from 'Components/SeccionPrincipalTorneos/SeccionPrincipalTorneos.js';
import {COLOR} from "Utils/consts";
import {useSelector} from 'react-redux';
import './App.css';
import Footer from 'Components/Footer/Footer';

function App() {
    const seccionPrincipalSeleccionada = useSelector(state => state.seccionPrincipalReducer.seccionPrincipal);

    if (!seccionPrincipalSeleccionada)
      return (
        <>
        <div className="app">
          <BannerHome titulo="Torneos" color={COLOR.ROJO} />
          <BannerHome titulo="Noticias" color={COLOR.VERDE} />
          <BannerHome titulo="Nosotros" color={COLOR.AZUL} />          
        </div>
        <Footer/>
        </>
      )
    else if (seccionPrincipalSeleccionada == "Torneos")
      return <SeccionPrincipalTorneos />
    return null;
}

export default App;
