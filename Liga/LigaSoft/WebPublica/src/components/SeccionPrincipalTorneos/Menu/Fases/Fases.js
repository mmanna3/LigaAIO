import React from "react";
import {actualizarFase} from 'Store/fase/action';
import {useDispatch, useSelector} from 'react-redux';
import styles from './Fases.css';
import baseStyles from 'GlobalStyle/base.css';
import bootstrap from 'GlobalStyle/bootstrap.min.css';

const Fases = () =>{
    
  const dispatch = useDispatch();
  const zona = useSelector(state => state.zonaReducer.zona);
  
  if (zona.zonaAperturaId != null && zona.zonaClausuraId == null) {
    dispatch(actualizarFase("Apertura"));
    return <></>;
  }
  else if (zona.zonaAperturaId == null && zona.zonaClausuraId != null) {
    dispatch(actualizarFase("Clausura"));
    return <></>;
  }  
  else {
    return (
      <div className={bootstrap.row}>
          <div className={baseStyles.cajaContainer}> 
            <div onClick={() => dispatch(actualizarFase("Apertura"))} className={styles.cajaFase}>
              <div className={baseStyles.textoCaja}>Apertura</div>
            </div>
          </div>
          <div className={baseStyles.cajaContainer}> 
            <div onClick={() => dispatch(actualizarFase("Clausura"))} className={styles.cajaFase}>
            <div className={baseStyles.textoCaja}>Clausura</div>
            </div>
          </div>
          <div className={baseStyles.cajaContainer}> 
            <div onClick={() => dispatch(actualizarFase("Anual"))} className={styles.cajaFase}>
            <div className={baseStyles.textoCaja}>Anual</div>
            </div>
          </div>
      </div>
    )
  }
}

export default Fases;