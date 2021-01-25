import React, { useState } from 'react';
import styles from './SeccionPrincipalFichaje.css';
import Input from './Input/Input';
import PasoInput from './PasoInput/PasoInput';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import Label from './Label/Label';
import PasoFotoCarnet from './PasoFotoCarnet/PasoFotoCarnet';
import { useForm } from 'react-hook-form';

const SeccionPrincipalFichaje = () => {

    const [codigoEquipo, setCodigoEquipo] = useState()
    const [codigoEquipoEsValido, setCodigoEquipoEsValido] = useState(null)
    const [nombreEquipo, setNombreEquipo] = useState("")
    const [yaValidoCodigoEquipo, setYaValidoCodigoEquipo] = useState(false)

    const { register, handleSubmit, errors } = useForm(); // initialize the hook
    
    const hacerElPost = async (data) => {
        fetch('publico/fichar', {
            method: 'POST',
            mode: 'cors',
            cache: 'no-cache',
            credentials: 'same-origin',
            headers: {'Content-Type': 'application/json'},
            redirect: 'follow',
            referrerPolicy: 'no-referrer',
            body: JSON.stringify(data)
        })
        .then(res => res.json())
        .then(res => {
          if (res.success) {
            console.log("Posteó piola")
          }else{
            console.log("Algún error posteando")
          }
        })
        .catch(function() {
            console.log("No le pudo pegar al back, capaz está caído");
        });        
    }

    const onSubmit = (data) => {
        console.log(data);
        hacerElPost(data)
    };
    
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
                    <form onSubmit={handleSubmit(onSubmit)}>


                        <div className={`${styles.primerPaso}`}>
                            <div className={`${bootstrap['form-group']} ${bootstrap.row}`}>
                                <div className={bootstrap['col-12']}> 
                                    <Label texto={"Código de tu equipo"} />
                                </div>
                                <div className={bootstrap['col-6']}> 
                                    <Input
                                        onChange={onCodigoEquipoChange}
                                        type="number"
                                        register={register} 
                                        name="codigoEquipo"
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
                        
                        
                        <PasoInput estiloDelPaso={styles.segundoPaso} register={register} name="nombre" titulo="Tu nombre" />
                        <PasoInput estiloDelPaso={styles.segundoPaso} register={register} name="apellido" titulo="Tu apellido" />
                        <PasoInput estiloDelPaso={styles.segundoPaso} register={register} name="dni" titulo="Tu DNI" />
                        

                        <PasoFotoCarnet estiloDelPaso={styles.tercerPaso} register={register}/>
                        
                        <input type="submit" />
                    </form>
                </div>
            </div>
    );
}

export default SeccionPrincipalFichaje;
