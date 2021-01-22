import React, { useState } from 'react';
import styles from './SeccionPrincipalFichaje.css';
import {fetchDataAndRenderResponse} from "Utils/hooks";
import Input from './Input/Input';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import Label from './Label/Label';

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
            <div className={styles.seccionContainer}>
                <div className={styles.seccion}>

                        <div className={`${bootstrap.row} ${styles.paso}`}>
                            <div className={bootstrap['col-12']}> 
                                <Label texto={"Código de tu equipo"} />
                            </div>
                            <div className={bootstrap['col-8']}> 
                                <Input
                                    onEnter={validarEquipo}
                                    type="number"
                                />
                            </div>
                            <div className={bootstrap['col-4']}> 
                                <button style={{width: "100%"}}>Validar</button>
                            </div>
                        </div>
                        

                        <div className={bootstrap.row}>
                            <div className={bootstrap['col-12']}> 
                                <Label texto={"Tu nombre"} />
                            </div>
                            <div className={bootstrap['col-12']}> 
                                <Input />
                            </div>                        
                        </div>
                        
                        {/* <ValidadorDeId id={2} /> */}
                </div>
            </div>
    );
}

export default SeccionPrincipalFichaje;
