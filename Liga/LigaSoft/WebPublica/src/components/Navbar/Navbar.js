import React, { useState }  from "react";
import styles from "./Navbar.css";
import bootstrap from "GlobalStyle/bootstrap.min.css";
import {actualizarSeccionPrincipal} from 'Store/seccion-principal/action';
import {actualizarTorneo} from 'Store/torneo/action';
import {actualizarZona} from 'Store/zona/action';
import {actualizarFase} from 'Store/fase/action';
import {actualizarOpcion} from 'Store/opcion/action';
import {useDispatch} from 'react-redux';

const Navbar = () => {
const [desplegarMenu, setDesplegarMenu] = useState(false);
const dispatch = useDispatch();

const menuClass = () => {
    if (desplegarMenu)
        return styles.menuDesplegado;
    else
        return styles.menuSinDesplegar;
};

const actualizarSeccionPrincipalYOcultarMenu = (seccionPrincipal) => {
    setDesplegarMenu(false); 
    dispatch(actualizarSeccionPrincipal(seccionPrincipal));
    dispatch(actualizarTorneo(""));
    dispatch(actualizarZona(""));
    dispatch(actualizarFase(""));
    dispatch(actualizarOpcion(""));
}

return (
        <nav className={styles.navbarYkn}>
            <div className={bootstrap.container}>
                <a className={bootstrap['navbar-brand']} href="#" onClick={() => actualizarSeccionPrincipalYOcultarMenu("")}>
                    <img className={styles.logoEdefi} alt="EDEFI" />
                </a>


                <button className={bootstrap['navbar-toggler']} type="button" data-toggle="collapse" onClick={() => setDesplegarMenu(!desplegarMenu)}>
                    <span className={styles['navbarTogglerIcon']}></span>
                </button>

                <div className={menuClass()}>
                    <ul className={bootstrap['navbar-nav']}>
                        <li className={bootstrap['nav-item']}>
                            <a className={styles.navLink} href="#" onClick={() => actualizarSeccionPrincipalYOcultarMenu("")}>Home</a>
                        </li>
                        <li className={bootstrap['nav-item']}>
                            <a className={styles.navLink} href="#" onClick={() => actualizarSeccionPrincipalYOcultarMenu("Torneos")}>Torneos</a>
                        </li>
                        <li className={bootstrap['nav-item']}>
                            <a className={styles.navLink} href="#" onClick={() => actualizarSeccionPrincipalYOcultarMenu("Noticias")}>Noticias</a>
                        </li>
                        <li className={bootstrap['nav-item']}>
                            <a className={styles.navLink} href="#" onClick={() => actualizarSeccionPrincipalYOcultarMenu("Nosotros")}>Nosotros</a>
                        </li>
                        <li className={bootstrap['nav-item']}>
                            <a className={styles.navLink} href="#" onClick={() => actualizarSeccionPrincipalYOcultarMenu("Contacto")}>Contacto</a>
                        </li>
                        <li className={bootstrap['nav-item']}>
                            <a className={styles.navLink} href="/Delegados" target="_blank">Delegados</a>
                        </li>
                    </ul>
                </div>

            </div>
        </nav>
    );
}

export default Navbar;