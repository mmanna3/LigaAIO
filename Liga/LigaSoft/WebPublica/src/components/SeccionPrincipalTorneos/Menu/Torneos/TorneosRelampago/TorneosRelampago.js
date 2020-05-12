import React from "react";
import {fetchDataAndRenderResponse} from "Utils/hooks";
import {actualizarTorneo} from 'Store/torneo/action';
import {actualizarFase} from 'Store/fase/action';
import {useDispatch} from 'react-redux';
import styles from './TorneosRelampago.css';
import baseStyles from 'GlobalStyle/base.css';
import bootstrap from 'GlobalStyle/bootstrap.min.css';


const TorneosRelampago = (props) => {
    
  const dispatch = useDispatch();

  const render = (data) => {

    var titulo;
    if (data.length > 0)
      titulo = <h3 className={styles.titulo}>Copas</h3>;

      return (
        <>
          {titulo}
          <div className={bootstrap.row}>
            {data.map(({ id, descripcion, formato }) => (
              <div key={id} className={baseStyles.cajaContainer}> 
                <div className={styles.cajaTorneo}
                    onClick={() => { dispatch(actualizarTorneo({"id": id, "descripcion": descripcion, "formato": formato })); dispatch(actualizarFase("Relampago"))}}>
                  <div className={baseStyles.textoCaja}>{descripcion}</div>
                </div>
              </div>
            ))}
          </div>
        </>
      )
    }    

    return fetchDataAndRenderResponse(`/publico/TorneosRelampago?anio=${props.anio}`, render);
}

export default TorneosRelampago;