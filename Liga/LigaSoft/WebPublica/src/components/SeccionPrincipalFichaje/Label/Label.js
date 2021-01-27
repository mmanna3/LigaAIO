import React from 'react';
import styles from './Label.css';

const Label = ({texto, subtitulo, centrado}) => {

  const estiloCentrado = centrado ? styles.centrado : "";
  
  return (
      <div className={estiloCentrado}>
        {
          subtitulo ?
            (
              <>
                <label className={styles.labelConSubtitulo}>{texto}</label> 
                <p className={styles.subtitulo}>{subtitulo}</p> 
              </>
            )
            :
            <label className={styles.labelSinSubtitulo}>{texto}</label>
        }        
      </div>
    )
}

export default Label;
