import React from 'react';
import estilos from './Error.css';
import bootstrap from "GlobalStyle/bootstrap.min.css";

const Error = ({errors, name, nombre}) => {

  return (
    <>
      {errors[name] && errors[name].type === 'required' &&
        <div className={bootstrap['col-12']}>
          <div className={`${bootstrap['alert']} ${bootstrap['alert-danger']} ${estilos.alertaValidacionEquipo}`}>
              ¡Ups! Te olvidaste tu {nombre}.
          </div>
        </div>
      }

      {errors[name] && errors[name].type !== 'required' &&
        <div className={bootstrap['col-12']}>
          <div className={`${bootstrap['alert']} ${bootstrap['alert-danger']} ${estilos.alertaValidacionEquipo}`}>
            {errors[name].message}
          </div>
        </div>
      }
    </>
    )
}

export default Error;
