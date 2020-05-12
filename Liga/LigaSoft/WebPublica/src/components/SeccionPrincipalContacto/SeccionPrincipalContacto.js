import React from 'react';
import styles from './SeccionPrincipalContacto.css';

const SeccionPrincipalContacto = () => {
    return (
            <div className={styles.seccion}>
                <h3 className={styles.titulo}>Contacto</h3>
                <div className={styles.contenido}>
                    <div className={styles.dato}>
                        <span className={styles.clave}>Horarios de atención: </span><span className={styles.valor}>Lunes a viernes de 17.30Hs a 20.30Hs.</span>                                                
                    </div>
                    <div className={styles.dato}>
                        <span className={styles.clave}>Dirección: </span><span className={styles.valor}>Juan B. Justo 550, Haedo. Bs. As. </span>                        
                    </div>
                    <div className={styles.dato}>
                        <span className={styles.clave}>CP: </span><span className={styles.valor}>1706</span>
                    </div>
                    <div className={styles.dato}>
                        <span className={styles.clave}>Teléfono: </span><span className={styles.valor}>2195-8389</span>
                    </div>
                    <div className={styles.dato}>
                        <a className={styles.clave} href="mailto:edefiargentina@hotmail.com">edefiargentina@hotmail.com</a>
                    </div>
                </div>
            </div>
    );
}

export default SeccionPrincipalContacto;
