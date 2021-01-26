import React, { useState } from 'react';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import Label from '../Label/Label';
import Input from '../Input/Input';
import Error from '../Error/Error';

const PasoInput = ({titulo, onChange, name, nombre, register, errors, estiloDelPaso}) => {

  return (
    <div className={estiloDelPaso}>
      <div className={bootstrap.row}>
          <div className={bootstrap['col-12']}> 
            <Label texto={titulo} />
          </div>
          <div className={bootstrap['col-12']}> 
            <Input type="text"
              register={register}
              name={name}
              type="text"
              onChange={onChange}
            />
          </div>
          <Error name={name} errors={errors} nombre={nombre}/>
      </div>
    </div>
    )
}

export default PasoInput;
