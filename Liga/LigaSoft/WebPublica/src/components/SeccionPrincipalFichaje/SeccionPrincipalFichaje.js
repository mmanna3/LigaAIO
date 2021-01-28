import React from 'react';
import styles from './SeccionPrincipalFichaje.css';
import PasoInput from './PasoInput/PasoInput';
import PasoCodigoEquipo from './PasoCodigoEquipo/PasoCodigoEquipo';
import PasoFotoCarnet from './PasoFotoCarnet/PasoFotoCarnet';
import PasoFotoDocumento from './PasoFotoDocumento/PasoFotoDocumento';
import { useForm } from 'react-hook-form';
import PasoBotonEnviar from './PasoBotonEnviar/PasoBotonEnviar';
import PasoFechaNacimiento from './PasoFechaNacimiento/PasoFechaNacimiento';
import bootstrap from "GlobalStyle/bootstrap.min.css";

const SeccionPrincipalFichaje = () => {

    const { register, handleSubmit, errors } = useForm(); // initialize the hook
    
    const hacerElPost = async (data) => {
        fetch('JugadorAutofichado/autofichaje', {
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

    const huboAlgunError = !(Object.keys(errors).length === 0 && errors.constructor === Object)

    return (
            <div className={styles.seccionContainer}>
                <div className={styles.seccion}>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        
                        {huboAlgunError &&
                            <div className={bootstrap['col-12']}>
                                <div className={`${bootstrap['alert']} ${bootstrap['alert-danger']} ${styles.alertaValidacion}`}>
                                    ¡Ups! Hubo algún error. Revisá tus datos y volvé a enviarlos.
                                </div>
                            </div>
                        }

                        <PasoCodigoEquipo estiloDelPaso={styles.pasoAzul} register={register} errors={errors}/>

                        <PasoInput  estiloDelPaso={styles.pasoRojo} 
                                    register={register} 
                                    errors={errors}
                                    longMaxima={14}
                                    name="nombre" 
                                    nombre="nombre" 
                                    titulo="Tu nombre" />
                        
                        <PasoInput  estiloDelPaso={styles.pasoRojo} 
                                    register={register} 
                                    errors={errors}
                                    longMaxima={14} 
                                    name="apellido" 
                                    nombre="apellido" 
                                    titulo="Tu apellido" />

                        <PasoInput  estiloDelPaso={styles.pasoRojo} 
                                    register={register} 
                                    errors={errors}
                                    longMaxima={9} 
                                    type="number"
                                    name="dni" 
                                    nombre="DNI" 
                                    titulo="Tu DNI" />
                        
                        <PasoFechaNacimiento  estiloDelPaso={styles.pasoRojo}
                                              register={register}
                                              errors={errors} />

                        <PasoFotoCarnet estiloDelPaso={styles.pasoVerde} errors={errors} register={register}/>
                        
                        <PasoFotoDocumento  estiloDelPaso={styles.pasoAzul} 
                                            register={register}
                                            titulo="Foto del frente de tu DNI"
                                            errors={errors}
                                            name="fotoDNIFrente"
                                            nombre="foto de FRENTE del DNI"
                                            />
                        
                        <PasoFotoDocumento  estiloDelPaso={styles.pasoAzul} 
                                            register={register}
                                            titulo="Foto de la parte de atrás de tu DNI"
                                            errors={errors}
                                            name="fotoDNIDorso"
                                            nombre="foto de ATRÁS del DNI"
                                            />
                        

                        <PasoBotonEnviar estiloDelPaso={styles.pasoRojo} />
                    </form>
                </div>
            </div>
    );
}

export default SeccionPrincipalFichaje;
