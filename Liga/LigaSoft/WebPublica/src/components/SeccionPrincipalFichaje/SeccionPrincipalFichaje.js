import React, { useState } from 'react';
import styles from './SeccionPrincipalFichaje.css';
import {fetchDataAndRenderResponse} from "Utils/hooks";
import Input from './Input/Input';

// const ValidadorDeId = ({id}) =>{    

//     const render = (data, loading) => {
  
//         if (!loading)
//             return (
//             <div className={styles.rowTablas}>            
//                 {data}
//             </div>
//             )
//         else
//             <div>Error</div>
//       }    
  
//       return fetchDataAndRenderResponse("/publico/ObtenerNombreDelEquipo?equipoId="+id, render);
//   }

const SeccionPrincipalFichaje = () => {
    function validarEquipo(id) {
        console.log(id);
    }
    
    return (
            <div className={styles.seccion}>                
                <Input 
                    label={"Código de tu equipo"}
                    onEnter={validarEquipo}
                    />
                {/* <ValidadorDeId id={2} /> */}
            </div>
    );
}

export default SeccionPrincipalFichaje;
