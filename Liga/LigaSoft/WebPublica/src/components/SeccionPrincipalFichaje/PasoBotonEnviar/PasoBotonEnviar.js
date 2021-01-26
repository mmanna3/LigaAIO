import React, { useState } from 'react';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import estilos from './PasoBotonEnviar.css';

const PasoBotonEnviar = ({estiloDelPaso}) => {

  return (
    <div className={`${estiloDelPaso}`}>
      <div className={`${bootstrap['form-group']} ${bootstrap.row}`}>
          <div className={bootstrap['col-12']}>
            <div className={estilos.contenedorDeContenidoCentrado}> 
               <button className={`${bootstrap.btn} ${bootstrap['btn-success']} ${estilos.boton}`} type="submit">ENVIAR MIS DATOS</button>
            </div>
          </div>
      </div>
  </div>
    )
}

export default PasoBotonEnviar;
