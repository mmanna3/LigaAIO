import React from "react";
import TorneosAperturaClausura from './TorneosAperturaClausura/TorneosAperturaClausura';
import TorneosRelampago from './TorneosRelampago/TorneosRelampago';
import styles from './Torneos.css'


const Torneos = () => {

  var anioActual = new Date().getFullYear();

  return <div>
    <TorneosAperturaClausura anio={anioActual} />
    <TorneosRelampago anio={anioActual}/>
    <div>Anio pasado</div>
    <TorneosAperturaClausura anio={anioActual-1} />
    <TorneosRelampago anio={anioActual-1}/>
  </div>
}

export default Torneos;