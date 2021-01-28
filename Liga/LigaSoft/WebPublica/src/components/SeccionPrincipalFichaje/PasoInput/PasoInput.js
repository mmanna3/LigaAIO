import React, { useState } from 'react';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import Label from '../Label/Label';
import Input from '../Input/Input';
import Error from '../Error/Error';

const PasoInput = ({titulo, onChange, name, nombre, longMaxima, register, errors, estiloDelPaso, type = "text"}) => {
  
  var caracteres = type === "text" ? 'letras' : 'números';

  return (
    <div className={estiloDelPaso}>
      <div className={bootstrap.row}>
          <div className={bootstrap['col-12']}> 
            <Label texto={titulo} />
          </div>
          <div className={bootstrap['col-12']}> 
            <Input 
              type={type}
              register={register({required: true, maxLength: {value: longMaxima, message: `¡Ups! Como máximo son ${longMaxima} ${caracteres}`} })}
              name={name}
              onChange={onChange}
            />
          </div>
          <Error name={name} errors={errors} nombre={nombre}/>
      </div>
    </div>
    )
}

export default PasoInput;
