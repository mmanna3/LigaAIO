import React from "react";
import TorneosAperturaClausura from './TorneosAperturaClausura/TorneosAperturaClausura';
import TorneosRelampago from './TorneosRelampago/TorneosRelampago';
import styles from './Torneos.css'


const Torneos = () => {
  return <div>
    <h4 className={styles.titulo}>Torneos Anuales</h4>
    <TorneosAperturaClausura/>
    <h4 className={styles.titulo}>Copas</h4>
    <TorneosRelampago/>
  </div>
}

export default Torneos;