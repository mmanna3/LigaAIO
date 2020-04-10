import React from "react";
import {fetchDataAndRenderResponse} from "Utils/hooks";
import {actualizarTorneo} from 'Store/torneo/action';
import {actualizarFase} from 'Store/fase/action';
import {useDispatch} from 'react-redux';
import styles from './Torneos.css';
import baseStyles from 'GlobalStyle/base.css';
import bootstrap from 'GlobalStyle/bootstrap.min.css';


const Torneos = () => {
    
  const dispatch = useDispatch();

  const render = (data) => {
      return (
        <div className={bootstrap.row}>
          {data.map(({ id, descripcion, formato }) => (
            <div key={id} className={baseStyles.cajaContainer}> 
              <div className={styles.cajaTorneo}
                   onClick={() => { dispatch(actualizarTorneo({"id": id, "descripcion": descripcion, "formato": formato}));                
                                    if (formato==="relampago")
                                      dispatch(actualizarFase("Relampago"))}}>
                <div className={baseStyles.textoCaja}>{descripcion}</div>
              </div>
            </div>
          ))}
        </div>
      )
    }    

    return fetchDataAndRenderResponse("/publico/torneos", render);
}

export default Torneos;