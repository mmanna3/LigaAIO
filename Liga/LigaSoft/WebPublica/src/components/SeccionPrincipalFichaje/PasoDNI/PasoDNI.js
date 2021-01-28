import React, { useState } from 'react';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import Label from '../Label/Label';
import Input from '../Input/Input';
import Error from '../Error/Error';
import Estilos from './PasoDNI.css'

const PasoDNI = ({register, errors, estiloDelPaso}) => {
  
  const jugadorYaEstaFichado = async (dni) => {
    return fetch(`/publico/elDniEstaFichado?dni=${dni}`)
        .then(response => response.json())
        .then(data => data)
        .catch(() => false)
  }

  const validar = async (dni) => {
    var resultado = await jugadorYaEstaFichado(dni)
    return !resultado || "¡Ups! Ya estás fichado. Consultá con tu delegado.";
  }   

  return (
    <div className={estiloDelPaso}>
      <div className={bootstrap.row}>
          <div className={bootstrap['col-12']}> 
            <Label texto="Tu DNI" />
          </div>
          <div className={bootstrap['col-12']}> 
            <Input 
              type="number"
              register={register({required: true, maxLength: {value: 9, message: `¡Ups! Como máximo son 9 números`}, validate: {asyncValidate: validar}})}
              name="dni"
              onChange={() => {}}
            />
          </div>
          <Error name="dni" errors={errors} nombre="DNI"/>
      </div>
    </div>
    )
}

export default PasoDNI;
