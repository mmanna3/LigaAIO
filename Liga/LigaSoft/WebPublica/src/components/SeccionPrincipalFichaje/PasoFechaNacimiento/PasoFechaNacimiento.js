import React, { useState } from 'react';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import Label from '../Label/Label';
import Input from '../Input/Input';
import Error from '../Error/Error';
import Estilos from './PasoFechaNacimiento.css'

const PasoFechaNacimiento = ({register, errors, estiloDelPaso}) => {

  const [valorCalculado, setValorCalculado] = useState("") 

  const actualizarDia = (dia) => {
    console.log(dia)
  }

  const actualizarMes = (mes) => {
    console.log(mes)
  }

  const actualizarAnio = (anio) => {
    console.log(anio)
  }

  return (
    <div className={estiloDelPaso}>
      <div className={bootstrap.row}>
          <div className={bootstrap['col-12']}> 
            <Label texto="Tu fecha de nacimiento" />
          </div>
          <div className={bootstrap['col-4']}> 
            <p className={Estilos.tituloInput}>Día</p>
            <Input 
              type="number"
              onChange={actualizarDia}
            />
          </div>
          <div className={bootstrap['col-4']}> 
            <p className={Estilos.tituloInput}>Mes</p>
            <Input 
              type="number"
              onChange={actualizarMes}
            />
          </div>
          <div className={bootstrap['col-4']}> 
            <p className={Estilos.tituloInput}>Año</p>
            <Input 
              type="number"
              onChange={actualizarAnio}
            />
          </div>
          <input 
              style={{display: 'none'}}
              ref={register({required: true})}
              name="fechaNacimiento" 
              defaultValue={valorCalculado}
          />

          <Error name="fechaNacimiento" errors={errors} nombre="fecha de nacimiento"/>
      </div>
    </div>
    )
}

export default PasoFechaNacimiento;
