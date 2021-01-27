import React from 'react';
import styles from './Label.css';

const Label = ({texto, subtitulo}) => {

  return (
      <div>
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
