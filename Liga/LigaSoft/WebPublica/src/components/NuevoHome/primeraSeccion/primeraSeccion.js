import React, { useState } from 'react';
import estilos from './primeraSeccion.css';
import bootstrap from './../../../assets/styles/bootstrap.min.css'

const primeraSeccion = () => {

    return (
    
        <div className={estilos.publicidadContenedor}>
            <div className={bootstrap.container}>
                <div className={bootstrap.row}>
                    <div className={bootstrap["col-sm"]}>
                        <div className={estilos.publicidad}></div>
                    </div>
                </div>                
            </div>            
        </div>
    )
}

export default primeraSeccion;
