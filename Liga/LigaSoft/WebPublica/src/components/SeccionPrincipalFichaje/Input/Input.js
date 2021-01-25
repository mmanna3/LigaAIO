import React, { useState } from 'react';
import styles from './Input.css';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import Label from '../Label/Label';

const Input = ({onChange, titulo, name, register, estiloDelPaso, type = "text"}) => {

  const [valor, setValor] = useState("")

  const handleOnChange = (e) => {
    // onChange(e.target.value); 
    setValor(e.target.value);
  }

  return (
    <div className={estiloDelPaso}>
    <div className={bootstrap.row}>
        <div className={bootstrap['col-12']}> 
          <Label texto={titulo} />
        </div>
        <div className={bootstrap['col-12']}> 
          <input type="text"
            ref={register}
            name={name}
            className={`${styles.input} ${bootstrap['form-control']}`}
            value={valor} type={type} 
            onChange={handleOnChange}
          />
        </div>
    </div>
    </div>
    )
}

export default Input;
