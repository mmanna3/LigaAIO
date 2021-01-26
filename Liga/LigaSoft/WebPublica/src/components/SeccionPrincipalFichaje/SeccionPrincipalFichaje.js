import React from 'react';
import styles from './SeccionPrincipalFichaje.css';
import PasoInput from './PasoInput/PasoInput';
import PasoCodigoEquipo from './PasoCodigoEquipo/PasoCodigoEquipo';
import PasoFotoCarnet from './PasoFotoCarnet/PasoFotoCarnet';
import PasoFotoDocumento from './PasoFotoDocumento/PasoFotoDocumento';
import { useForm } from 'react-hook-form';
import PasoBotonEnviar from './PasoBotonEnviar/PasoBotonEnviar';

const SeccionPrincipalFichaje = () => {

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

    return (
            <div className={styles.seccionContainer}>
                <div className={styles.seccion}>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        
                        <PasoCodigoEquipo estiloDelPaso={styles.primerPaso} register={register({required: true})} errors={errors} />

                        <PasoInput estiloDelPaso={styles.segundoPaso} register={register({required: true})} errors={errors} name="nombre" titulo="Tu nombre" />
                        <PasoInput estiloDelPaso={styles.segundoPaso} register={register({required: true})} errors={errors} name="apellido" titulo="Tu apellido" />
                        <PasoInput estiloDelPaso={styles.segundoPaso} register={register({required: true})} errors={errors} name="dni" titulo="Tu DNI" />
                        

                        <PasoFotoCarnet estiloDelPaso={styles.tercerPaso} register={register}/>
                        <PasoFotoDocumento estiloDelPaso={styles.primerPaso} register={register}/>
                        

                        <PasoBotonEnviar estiloDelPaso={styles.segundoPaso} />
                    </form>
                </div>
            </div>
    );
}

export default SeccionPrincipalFichaje;
