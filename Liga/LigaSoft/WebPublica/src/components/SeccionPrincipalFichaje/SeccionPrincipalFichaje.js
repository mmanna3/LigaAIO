import React from 'react';
import styles from './SeccionPrincipalFichaje.css';
import PasoInput from './PasoInput/PasoInput';
import PasoCodigoEquipo from './PasoCodigoEquipo/PasoCodigoEquipo';
import PasoFotoCarnet from './PasoFotoCarnet/PasoFotoCarnet';
import { useForm } from 'react-hook-form';

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
                        
                        <PasoCodigoEquipo estiloDelPaso={styles.primerPaso} register={register} />

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
