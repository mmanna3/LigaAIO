import React, { useState } from 'react';
import styles from './Input.css';
import bootstrap from "GlobalStyle/bootstrap.min.css";

const Input = ({onChange = () => {}, name, register, type = "text"}) => {

  const [valor, setValor] = useState("")

  const handleOnChange = (e) => {
    onChange(e.target.value); 
    setValor(e.target.value);
  }

  return (
          <input
            ref={register}
            name={name}
            className={`${styles.input} ${bootstrap['form-control']}`}
            value={valor} 
            type={type} 
            onChange={handleOnChange}
            autoComplete="off"
          />
    )
}

export default Input;
