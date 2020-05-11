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
          <div className={bootstrap.row}>
              <div className={bootstrap['col-md-12']}>
                  <div className={styles.contenido}> 
                      <div className={styles.titulo}><span className={styles.tituloFecha}>{data.fecha} </span><span className={styles.tituloSeparador}>|</span> <span className={styles.tituloTexto}>{data.titulo}</span></div>
                      <div className={styles.subtitulo}>{data.subtitulo}</div>
                      <div className={styles.cuerpo} dangerouslySetInnerHTML={{__html: data.cuerpo}}></div>
                  </div>
              </div>
          </div>
      </div>
    )
  }    

  return fetchDataAndRenderResponse('/publico/noticia?id='+ noticiaSeleccionada.id, render);
}

export default NoticiaSeleccionada;
