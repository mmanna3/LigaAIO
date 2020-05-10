import React from 'react';
import {fetchDataAndRenderResponse} from "Utils/hooks";
import bootstrap from 'GlobalStyle/bootstrap.min.css';
import styles from './SeccionPrincipalNoticias.css';

const SeccionPrincipalNoticias = () => {    
  
    const render = (data) => {      
      
      return (
          <>
          <div className={bootstrap.row}>
            {data.map(({ id, titulo, subtitulo, fecha }) => (
              <div key={id}> 
                <div>{fecha} | {titulo}</div>
                <div>{subtitulo}</div>
              </div>
            ))}
          </div>
          </>
        )
      }    
  
      return fetchDataAndRenderResponse(`/publico/noticias`, render);
  }

export default SeccionPrincipalNoticias;
