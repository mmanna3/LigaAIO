import React from "react";
import TorneosAperturaClausura from './TorneosAperturaClausura/TorneosAperturaClausura';
import TorneosRelampago from './TorneosRelampago/TorneosRelampago';
import styles from './Torneos.css'


const Torneos = () => {

  var anioActual = new Date().getFullYear();

  const [mostrarTorneosDelAnioPasado, setMostrarTorneosDelAnioPasado] = React.useState(false);
  const onClick = () => setMostrarTorneosDelAnioPasado(true);

  return <div>
    <TorneosAperturaClausura anio={anioActual} />
    <TorneosRelampago anio={anioActual}/>
    
    <div>      
      {mostrarTorneosDelAnioPasado ? 
      <>
        <h3 className={styles.anioPasado}>Año {anioActual-1}</h3>
        <TorneosAperturaClausura anio={anioActual-1} />
        <TorneosRelampago anio={anioActual-1}/>
      </> : 
      <div className={styles.caja} onClick={onClick}>
        <div className={styles.textoOpcionCentrado}>
          Ver torneos del año pasado
        </div>
      </div>
      }
    </div>
  </div>
}

export default Torneos;