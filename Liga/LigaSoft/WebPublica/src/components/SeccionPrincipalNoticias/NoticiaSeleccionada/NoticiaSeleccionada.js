import React from 'react';
import {actualizarNoticia} from 'Store/noticia/action';
import {actualizarSeccionPrincipal} from 'Store/seccion-principal/action';
import {useDispatch} from 'react-redux';
import {fetchDataAndRenderResponse} from "Utils/hooks";
import bootstrap from 'GlobalStyle/bootstrap.min.css';
import styles from './NoticiaSeleccionada.css';
import {useSelector} from 'react-redux';

const NoticiaSeleccionada = () => {    
  const noticiaSeleccionada = useSelector(state => state.noticiaReducer.noticia);

  const render = (data) => {      
    return (
      <div className={styles.seccion}>
        {data.titulo}
        {noticiaSeleccionada.subtitulo}
          {/* <div key={id}>
              <div className={bootstrap.row}>
                  <div className={bootstrap['col-md-12']}>                
                      <div className={styles.contenido}
                          onClick={() => { dispatch(actualizarSeccionPrincipal("NoticiaSeleccionada")); dispatch(actualizarNoticia({"id": id, "titulo": titulo, "subtitulo": subtitulo, "fecha": fecha }));}}> 
                          <div className={styles.titulo}><span className={styles.tituloFecha}>{fecha} </span><span className={styles.tituloSeparador}>|</span> <span className={styles.tituloTexto}>{titulo}</span></div>
                          <div>{subtitulo} <span className={styles.puntosSuspensivos}>...</span></div>                            
                      </div>
                  </div>
              </div>
              <div className={styles.separador}></div>
          </div> */}
      </div>
    )
  }    

  return fetchDataAndRenderResponse('/publico/noticia?id='+ noticiaSeleccionada.id, render);
}

export default NoticiaSeleccionada;
