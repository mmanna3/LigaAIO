import React from 'react';
import styles from './Label.css';

const Label = ({texto}) => {

  return (
      <div>        
        <label className={styles.label}>{texto}</label>
      </div>
    )
}

export default Label;
