import React from "react";
import {actualizarOpcion} from 'Store/opcion/action';
import {useDispatch} from 'react-redux';
import styles from './Opciones.css';
import baseStyles from 'GlobalStyle/base.css';
import bootstrap from 'GlobalStyle/bootstrap.min.css';

const Opciones = () =>{
    
  const dispatch = useDispatch();

  return (
    <>
      <div className={bootstrap.row}>
        <div className={styles.cajaContainer}>
          <div onClick={() => dispatch(actualizarOpcion("Posiciones"))} className={styles.cajaOpciones}>
            <div className={baseStyles.textoCaja}>Posiciones</div>
          </div>
        </div>
        <div className={styles.cajaContainer}>
          <div onClick={() => dispatch(actualizarOpcion("Fixture"))} className={styles.cajaOpciones}>
            <div className={baseStyles.textoCaja}>Fixture</div>
          </div>
        </div>
      </div>
      <div className={bootstrap.row}>
      <div className={styles.cajaContainer}>
          <div onClick={() => dispatch(actualizarOpcion("Jornadas"))} className={styles.cajaOpciones}>
            <div className={baseStyles.textoCaja}>Jornadas</div>
          </div>
        </div>
        <div className={styles.cajaContainer}>
          <div onClick={() => dispatch(actualizarOpcion("Clubes"))} className={styles.cajaOpciones}>
            <div className={baseStyles.textoCaja}>Clubes</div>
          </div>
        </div>
      </div>
    </>
  )
}

export default Opciones;