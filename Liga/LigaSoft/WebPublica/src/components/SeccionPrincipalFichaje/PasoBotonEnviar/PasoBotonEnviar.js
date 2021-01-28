import React, { useState } from 'react';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import estilos from './PasoBotonEnviar.css';

const PasoBotonEnviar = ({estiloDelPaso}) => {

  return (
    <div className={`${estiloDelPaso}`}>
      <div className={`${bootstrap['form-group']} ${bootstrap.row}`}>
          <div className={bootstrap['col-12']}>
            
            <div className={estilos.contenedorDeContenidoCentrado}> 
               <p className={estilos.declaracion}>Al enviar los datos, declaro estar acompañado por un mayor de edad que autoriza a que puedan publicarse fotos y videos del jugador fichado en medios donde se difunda material sobre torneos orgnaizados por EDEFI.</p>
            </div>
            
            <div className={estilos.contenedorDeContenidoCentrado}> 
               <button className={`${bootstrap.btn} ${bootstrap['btn-success']} ${estilos.boton}`} type="submit">ENVIAR MIS DATOS</button>
            </div>
          </div>
      </div>
  </div>
    )
}

export default PasoBotonEnviar;
