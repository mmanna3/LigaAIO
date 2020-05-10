import React from 'react';
import {fetchDataAndRenderResponse} from "Utils/hooks";
import bootstrap from 'GlobalStyle/bootstrap.min.css';
import styles from './SeccionPrincipalNoticias.css';

const SeccionPrincipalNoticias = () => {    
  
    const render = (data) => {      
      
      return (
          <div className={styles.seccion}>
          {data.map(({ id, titulo, subtitulo, fecha }) => (
            <div>
                <div className={bootstrap.row}>
                    <div className={bootstrap['col-md-12']}>                
                        <div key={id} className={styles.contenido}> 
                            <div className={styles.titulo}><span className={styles.tituloFecha}>{fecha} </span><span className={styles.tituloSeparador}>|</span> <span className={styles.tituloTexto}>{titulo}</span></div>
                            <div>{subtitulo} <span className={styles.puntosSuspensivos}>...</span></div>                            
                        </div>
                    </div>
                </div>
                <div className={styles.separador}></div>
            </div>
          ))}          
          </div>
        )
      }    
  
      return fetchDataAndRenderResponse('/publico/noticias', render);
  }

export default SeccionPrincipalNoticias;
