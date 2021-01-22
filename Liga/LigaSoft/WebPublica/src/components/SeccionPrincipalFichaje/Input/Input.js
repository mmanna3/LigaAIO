import React, { useState } from 'react';
import styles from './Input.css';

const Input = ({onEnter, label}) => {

  const [valor, setValor] = useState("")

  const handleKeyDown = (event) => {
      if (event.key === 'Enter') {
          onEnter(valor)
      }
  }

  return (
      <div>
        <label className={styles.label}>{label}</label>
        <input type="text" value={valor} onChange={(e) => setValor(e.target.value)} onKeyDown={handleKeyDown} />
      </div>
    )
}

export default Input;
