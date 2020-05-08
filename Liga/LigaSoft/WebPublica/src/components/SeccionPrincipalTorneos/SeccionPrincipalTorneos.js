import React from 'react';
import Menu from './Menu/Menu';
import Navegacion from './Navegacion/Navegacion';
import styles from './SeccionPrincipalTorneos.css';
import bootstrap from "GlobalStyle/bootstrap.min.css";

const SeccionPrincipalTorneos = () => {
    return (<div className={styles.seccionPrincipalTorneos}>
                <div className={bootstrap.row}>
                    <Navegacion/>
                </div>
                <div className={styles.marginTop1em}>
                    <Menu />
                </div>
            </div>);
}

export default SeccionPrincipalTorneos;
