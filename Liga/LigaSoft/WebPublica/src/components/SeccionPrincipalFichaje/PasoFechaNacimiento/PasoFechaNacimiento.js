import React, { useState, useEffect } from 'react';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import Label from '../Label/Label';
import Input from '../Input/Input';
import Estilos from './PasoFechaNacimiento.css'

const PasoFechaNacimiento = ({register, errors, estiloDelPaso}) => {

  const [valorCalculado, setValorCalculado] = useState()
  const [dia, setDia] = useState()
  const [mes, setMes] = useState()
  const [anio, setAnio] = useState()

  useEffect(() => {
    setValorCalculado(`${dia}-${mes}-${anio}`);
  }, [dia, mes, anio])
  
  const actualizarDia = (dia) => {
    setDia(dia)
  }

  const actualizarMes = (mes) => {
    setMes(mes)
  }

  const actualizarAnio = (anio) => {
    setAnio(anio)
  }

  const validarFecha = date => {
    console.log(date)
    var temp = date.split('-');
    var d = new Date(temp[1] + '-' + temp[0] + '-' + temp[2]);
    var resultado = (d && (d.getMonth() + 1) == temp[1] && d.getDate() == Number(temp[0]) && d.getFullYear() == Number(temp[2]));
    return resultado;
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
              ref={register({required: true, validate: validarFecha})}
              name="fechaNacimiento" 
              defaultValue={valorCalculado}
          />
          
          {errors.fechaNacimiento && 
            <div className={bootstrap['col-12']}>
              <div className={`${bootstrap['alert']} ${bootstrap['alert-danger']} ${Estilos.alertaValidacionEquipo}`}>
                  ¡Ups! Hay un problema con la <strong>fecha</strong>. Revisala.
              </div>
            </div>
        }
      </div>
    </div>
    )
}

export default PasoFechaNacimiento;
