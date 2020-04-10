import React from "react";
import {fetchDataAndRenderResponse} from "Utils/hooks";
import {actualizarZona} from 'Store/zona/action';
import {useDispatch} from 'react-redux';
import styles from './Zonas.css';
import baseStyles from 'GlobalStyle/base.css';
import bootstrap from 'GlobalStyle/bootstrap.min.css';

const Zonas = (props) =>{
      
  const dispatch = useDispatch();

  const render = (data) => {
      return (
        <div className={bootstrap.row}>
          {data.map(({ id, descripcion }) => (
            <div key={id} className={baseStyles.cajaContainer}> 
              <div className={styles.cajaZona}
                   onClick={() => dispatch(actualizarZona({"id": id, "descripcion": descripcion}))}>
                    <div className={baseStyles.textoCaja}>{descripcion}</div>
              </div>
            </div>
          ))}
        </div>
      )
    }    

    return fetchDataAndRenderResponse("/publico/zonas?torneoId="+props.torneoId, render);
}

export default Zonas;