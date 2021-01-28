import React from 'react';
import styles from './SeccionPrincipalFichaje.css';
import PasoInput from './PasoInput/PasoInput';
import PasoCodigoEquipo from './PasoCodigoEquipo/PasoCodigoEquipo';
import PasoFotoCarnet from './PasoFotoCarnet/PasoFotoCarnet';
import PasoFotoDocumento from './PasoFotoDocumento/PasoFotoDocumento';
import { useForm } from 'react-hook-form';
import PasoBotonEnviar from './PasoBotonEnviar/PasoBotonEnviar';
import PasoFechaNacimiento from './PasoFechaNacimiento/PasoFechaNacimiento';

const SeccionPrincipalFichaje = () => {

    const { register, handleSubmit, errors } = useForm(); // initialize the hook
    
    const hacerElPost = async (data) => {
        fetch('publico/autofichaje', {
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

    return (
            <div className={styles.seccionContainer}>
                <div className={styles.seccion}>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        
                        <PasoCodigoEquipo estiloDelPaso={styles.pasoAzul} register={register({required: true})} errors={errors}/>

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
