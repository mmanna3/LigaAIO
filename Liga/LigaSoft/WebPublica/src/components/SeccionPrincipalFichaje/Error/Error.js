import React from 'react';
import estilos from './Error.css';
import bootstrap from "GlobalStyle/bootstrap.min.css";

const Error = ({errors, name, nombre}) => {
  //Yo sé que esto está bien feo, disculpá
  return (
    <>
      {errors[name] && errors[name].type === 'required' &&
        <div className={bootstrap['col-12']}>
          <div className={`${bootstrap['alert']} ${bootstrap['alert-danger']} ${estilos.alertaValidacionEquipo}`}>
              ¡Ups! Te olvidaste tu <strong>{nombre}</strong>
          </div>
        </div>
      }

      {errors[name] && errors[name].type === 'validate' &&
        <div className={bootstrap['col-12']}>
          <div className={`${bootstrap['alert']} ${bootstrap['alert-danger']} ${estilos.alertaValidacionEquipo}`}>
            ¡Ups! Te olvidaste tu <strong>{nombre}</strong>
          </div>
        </div>
      }

      {errors[name] && errors[name].type === 'maxLength' &&
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
