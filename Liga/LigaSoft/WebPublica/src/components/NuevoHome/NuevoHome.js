import React, { useState } from 'react';
import estilos from './NuevoHome.css';
import bootstrap from './../../assets/styles/bootstrap.min.css'
import SegundaSeccion from './segundaSeccion/segundaSeccion';

const NuevoHome = () => {

    return (
    <div>
        <div className={estilos.publicidadContenedor}>
            <div className={bootstrap.container}>
                <div className={bootstrap.row}>
                    <div className={bootstrap["col-sm"]}>
                        <div className={estilos.publicidad}></div>
                    </div>
                </div>                
            </div>            
        </div>
        <SegundaSeccion/>
        <div className={estilos.noticiasContenedor}></div>
    
    </div>)
}

export default NuevoHome;
