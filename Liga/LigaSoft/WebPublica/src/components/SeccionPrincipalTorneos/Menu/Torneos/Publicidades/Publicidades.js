import React from "react";
import {fetchDataAndRenderResponse} from "Utils/hooks";
import styles from './Publicidades.css';
import bootstrap from 'GlobalStyle/bootstrap.min.css';


const Publicidades = () => {    

  const render = (data) => {
    
    return (
        <>
        <div className={bootstrap.row}>          
            {data.map(({ id, urlDestino, imgSrc, titulo }) => (
              <div className={styles.columna}>
                <div key={id} className={styles.contenedor}>
                  <img className={styles.publicidad} src={imgSrc} width={480} height={180} />
                </div>
              </div>
            ))}          
        </div>
        </>
      )
    }    

    return fetchDataAndRenderResponse(`/publico/publicidades`, render);
}

export default Publicidades;