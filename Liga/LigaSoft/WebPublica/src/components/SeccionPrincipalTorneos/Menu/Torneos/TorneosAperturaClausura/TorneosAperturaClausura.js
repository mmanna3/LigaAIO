import React from "react";
import {fetchDataAndRenderResponse} from "Utils/hooks";
import {actualizarTorneo} from 'Store/torneo/action';
import {useDispatch} from 'react-redux';
import styles from './TorneosAperturaClausura.css';
import baseStyles from 'GlobalStyle/base.css';
import bootstrap from 'GlobalStyle/bootstrap.min.css';


const TorneosAperturaClausura = (props) => {
    
  const dispatch = useDispatch();

  const render = (data) => {
    
    var titulo;
    if (data.length > 0)
      titulo = <h4 className={styles.titulo}>Torneos Anuales</h4>;
    
    return (
        <>
        {titulo}
        <div className={bootstrap.row}>
          {data.map(({ id, descripcion, formato }) => (
            <div key={id} className={baseStyles.cajaContainer}> 
              <div className={styles.cajaTorneo}
                   onClick={() => { dispatch(actualizarTorneo({"id": id, "descripcion": descripcion, "formato": formato }));}}>
                <div className={baseStyles.textoCaja}>{descripcion}</div>
              </div>
            </div>
          ))}
        </div>
        </>
      )
    }    

    return fetchDataAndRenderResponse(`/publico/TorneosAperturaClausura?anio=${props.anio}`, render);
}

export default TorneosAperturaClausura;