import React, { useState } from 'react';
import styles from './Input.css';

const Input = ({onEnter, type = "text"}) => {

  const [valor, setValor] = useState("")

  const handleKeyDown = (event) => {
      if (event.key === 'Enter') {
          onEnter(valor)
      }
  }

  return (
      <div>        
        <input type="text" className={styles.input} value={valor} type={type} onChange={(e) => setValor(e.target.value)} onKeyDown={handleKeyDown} />
      </div>
    )
}

export default Input;
