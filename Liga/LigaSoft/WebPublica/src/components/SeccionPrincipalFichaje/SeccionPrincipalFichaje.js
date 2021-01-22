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

                        <div className={`${styles.primerPaso}`}>
                            <div className={`${bootstrap['form-group']} ${bootstrap.row}`}>
                                <div className={bootstrap['col-12']}> 
                                    <Label texto={"Código de tu equipo"} />
                                </div>
                                <div className={bootstrap['col-6']}> 
                                    <Input
                                        onChange={onCodigoEquipoChange}
                                        type="number"
                                    />
                                </div>
                                <div className={bootstrap['col-6']}> 
                                    <button className={`${bootstrap.btn} ${bootstrap['btn-success']}`} style={{width: "100%"}} onClick={onValidarClick}>Validar</button>
                                </div>
                                {
                                    yaValidoCodigoEquipo &&
                                    (codigoEquipoEsValido ?
                                        <div className={bootstrap['col-12']}>
                                            <div className={`${bootstrap['alert']} ${bootstrap['alert-success']} ${styles.alertaValidacionEquipo}`}>
                                                Tu equipo es <strong>{nombreEquipo}</strong>
                                            </div>
                                        </div> :
                                        <div className={bootstrap['col-12']}>
                                            <div className={`${bootstrap['alert']} ${bootstrap['alert-danger']} ${styles.alertaValidacionEquipo}`}>
                                                El código es incorrecto
                                            </div>
                                        </div>
                                    )
                                }
                            </div>
                        </div>
                        
                        <div className={styles.segundoPaso}>
                            <div className={bootstrap.row}>
                                <div className={bootstrap['col-12']}> 
                                    <Label texto={"Tu nombre"} />
                                </div>
                                <div className={bootstrap['col-12']}> 
                                    <Input />
                                </div>                        
                            </div>
                        </div>


                </div>
            </div>
    );
}

export default SeccionPrincipalFichaje;
