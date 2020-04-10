import React from "react";
import {useSelector} from 'react-redux';
import {useDispatch} from 'react-redux';
import {actualizarTorneo} from 'Store/torneo/action';
import {actualizarZona} from 'Store/zona/action';
import {actualizarFase} from 'Store/fase/action';
import {actualizarOpcion} from 'Store/opcion/action';
import styles from "./Navegacion.css";

const Navegacion = () =>{  

  const torneoSeleccionado = useSelector(state => state.torneoReducer.torneo);
  const zonaSeleccionada = useSelector(state => state.zonaReducer.zona);
  const faseSeleccionada = useSelector(state => state.faseReducer.fase);
  const opcionSeleccionada = useSelector(state => state.opcionReducer.opcion);

  const dispatch = useDispatch();

  const deseleccionarTorneo = () => {
    dispatch(actualizarTorneo(""));
    dispatch(actualizarZona(""));
    dispatch(actualizarFase(""));
    dispatch(actualizarOpcion(""));
  }

  const deseleccionarZona = () => {
    dispatch(actualizarZona(""));
    if (faseSeleccionada != "Relampago")
      dispatch(actualizarFase(""));
    dispatch(actualizarOpcion(""));
  }

  const deseleccionarFase = () => {
    dispatch(actualizarFase(""));
    dispatch(actualizarOpcion(""));
  }

  const deseleccionarOpcion = () => {
    dispatch(actualizarOpcion(""));
  }

  const widthClassTorneoYZona = () => {
      if (faseSeleccionada == "Relampago")
        return styles.widthTorneoYZonaSinFase;
      else
        return styles.widthTorneoYZonaConFase;
  }

  const widthClassOpcion = () => {    
    if (faseSeleccionada == "Relampago")
      return styles.widthOpcionSinFase;
    else
      return styles.widthOpcionConFase;
  }

  const classTextoOpcion = () => {    
    if (faseSeleccionada == "Relampago")
      return styles.textoOpcionCentrado;
    else
      return styles.textoOpcion
  }

  let result = [];
  
  if (torneoSeleccionado)
    result.push(<div key="torneo" className={widthClassTorneoYZona()}>
                  <div className={styles.cajaNavegacionRoja} 
                       onClick={() => deseleccionarTorneo()}>
                          <div className={styles.texto}>
                            {torneoSeleccionado.descripcion}
                          </div>
                  </div>
                </div>);
  if (zonaSeleccionada)
    result.push(<div key="zona" className={widthClassTorneoYZona()}>
                  <div className={styles.cajaNavegacionVerde} 
                       onClick={() => deseleccionarZona()}>
                        <div className={styles.texto}>
                          {zonaSeleccionada.descripcion}
                        </div>
                  </div>
                </div>);
  if (faseSeleccionada && faseSeleccionada != "Relampago")
    result.push(<div key="fase" className={styles.widthFase}>
                  <div className={styles.cajaNavegacionAzul} 
                       onClick={() => deseleccionarFase()}>
                      <div className={styles.texto}>
                        {faseSeleccionada}
                      </div>
                  </div>
                </div>);
  if (opcionSeleccionada)
    result.push(<div key="opcion" className={widthClassOpcion()}>
                  <div className={styles.cajaNavegacionAzul} 
                       onClick={() => deseleccionarOpcion()}>
                      <div className={classTextoOpcion()}>
                        {opcionSeleccionada}
                      </div>
                  </div>
                </div>);
  
  return result;
}

export default Navegacion;