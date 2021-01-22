import React, { useState } from 'react';
import styles from './Input.css';
import bootstrap from "GlobalStyle/bootstrap.min.css";

const Input = ({onChange, type = "text"}) => {

  const [valor, setValor] = useState("")

  const handleOnChange = (e) => {
    onChange(e.target.value); 
    setValor(e.target.value);
  }

  return (
      <div>        
        <input type="text" 
          className={`${styles.input} ${bootstrap['form-control']}`}
          value={valor} type={type} 
          onChange={handleOnChange}
        />
      </div>
    )
}

export default Input;
