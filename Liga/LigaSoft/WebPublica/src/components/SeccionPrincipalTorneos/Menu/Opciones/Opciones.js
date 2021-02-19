import React from "react";
import {actualizarOpcion} from 'Store/opcion/action';
import {useDispatch} from 'react-redux';
import styles from './Opciones.css';
import baseStyles from 'GlobalStyle/base.css';
import bootstrap from 'GlobalStyle/bootstrap.min.css';
import {useSelector} from 'react-redux';

const Opciones = () =>{
    
  const dispatch = useDispatch();
  const faseSeleccionada = useSelector(state => state.faseReducer.fase);
  
  if (faseSeleccionada == 'Anual') {
    dispatch(actualizarOpcion("Posiciones"));
    return <></>;
  }
    

  return (
    <>
      <div className={bootstrap.row}>
        <div className={styles.cajaContainer}>
          <div onClick={() => dispatch(actualizarOpcion("Posiciones"))} className={styles.cajaOpciones}>
            <div className={baseStyles.textoCaja}>Posiciones <span className={styles.iconoPosiciones}><i className="fa fa-star fa-lg"></i></span></div>
          </div>
        </div>
        <div className={styles.cajaContainer}>
          <div onClick={() => dispatch(actualizarOpcion("Fixture"))} className={styles.cajaOpciones}>
            <div className={baseStyles.textoCaja}>Fixture <span className={styles.iconoPosiciones}><i className="fa fa-trophy fa-lg"></i></span></div>
          </div>
        </div>
      </div>
      <div className={bootstrap.row}>
      <div className={styles.cajaContainer}>
          <div onClick={() => dispatch(actualizarOpcion("Jornadas"))} className={styles.cajaOpciones}>
            <div className={baseStyles.textoCaja}>Jornadas <span className={styles.iconoPosiciones}><i className="fa fa-table fa-lg"></i></span></div>
          </div>
        </div>
        <div className={styles.cajaContainer}>
          <div onClick={() => dispatch(actualizarOpcion("Clubes"))} className={styles.cajaOpciones}>
            <div className={baseStyles.textoCaja}>Clubes <span className={styles.iconoPosiciones}><i className="fa fa-map-marker fa-lg"></i></span></div>
          </div>
        </div>
        <div className={styles.cajaContainer}>
          <div onClick={() => dispatch(actualizarOpcion("Sanciones"))} className={styles.cajaOpciones}>
            <div className={baseStyles.textoCaja}>Sanciones <span className={styles.iconoPosiciones}><i className="fa fa-exclamation fa-lg"></i></span></div>
          </div>
        </div>
      </div>
    </>
  )
}

export default Opciones;