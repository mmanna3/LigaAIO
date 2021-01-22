import React, { useState } from 'react';
import styles from './SeccionPrincipalFichaje.css';
import Input from './Input/Input';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import Label from './Label/Label';

const SeccionPrincipalFichaje = () => {

    const [codigoEquipo, setCodigoEquipo] = useState()
    const [codigoEquipoEsValido, setCodigoEquipoEsValido] = useState(null)
    const [nombreEquipo, setNombreEquipo] = useState("")
    const [yaValidoCodigoEquipo, setYaValidoCodigoEquipo] = useState(false)

    const onCodigoEquipoChange = (id) => {
        setCodigoEquipo(id)
    }

    const onValidarClick = () => {
        fetch(`/publico/obtenerNombreDelEquipo?equipoId=${codigoEquipo}`)
            .then(response => response.json())
            .then(data => {setNombreEquipo(data); setCodigoEquipoEsValido(true); setYaValidoCodigoEquipo(true)})
            .catch(() => {setCodigoEquipoEsValido(false); setYaValidoCodigoEquipo(true)})
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
                                    onChange={onCodigoEquipoChange}
                                    type="number"                                    
                                />
                            </div>
                            <div className={bootstrap['col-4']}> 
                                <button style={{width: "100%"}} onClick={onValidarClick}>Validar</button>
                            </div>
                            {
                                yaValidoCodigoEquipo &&
                                (codigoEquipoEsValido ?
                                    <div className={bootstrap['col-12']}>
                                        Tu equipo es {nombreEquipo}
                                    </div> :
                                    <div className={bootstrap['col-12']}>
                                        El código no pertenece a ningún equipo
                                    </div>
                                )
                            }
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
