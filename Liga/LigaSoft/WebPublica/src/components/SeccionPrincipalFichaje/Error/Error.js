import React from 'react';
import estilos from './Error.css';
import bootstrap from "GlobalStyle/bootstrap.min.css";

const Error = ({errors, name, nombre}) => {

  return (
    <>
      {errors[name] && 
        <div className={bootstrap['col-12']}>
          <div className={`${bootstrap['alert']} ${bootstrap['alert-danger']} ${estilos.alertaValidacionEquipo}`}>
              ¡Ups! Te olvidaste tu <strong>{nombre}</strong>
          </div>
        </div>
        }
    </>
    )
}

export default Error;
