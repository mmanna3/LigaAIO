import React, { useState }  from "react";
import styles from "./Navbar.css";
import bootstrap from "GlobalStyle/bootstrap.min.css";

const Navbar = () => {
const [desplegarMenu, setDesplegarMenu] = useState(false);

const menuClass = () => {
    if (desplegarMenu)
        return styles.menuDesplegado;
    else
        return styles.menuSinDesplegar;
};

  return (
            <nav className={styles.navbarYkn}>
                <div className={bootstrap.container}>
                    <a className={bootstrap['navbar-brand']} href="/publico/index">
                        <img className={styles.logoEdefi} alt="EDEFI" />
                    </a>


                    <button className={bootstrap['navbar-toggler']} type="button" data-toggle="collapse" onClick={() => setDesplegarMenu(!desplegarMenu)}>
                        <span className={styles['navbarTogglerIcon']}></span>
                    </button>

                    <div className={menuClass()}>
                        <ul className={bootstrap['navbar-nav']}>
                            <li className={bootstrap['nav-item']}>
                                <a className={styles.navLink} href="#">Home</a>
                            </li>
                            <li className={bootstrap['nav-item']}>
                                <a className={styles.navLink} href="#">Noticias</a>
                            </li>
                            <li className={bootstrap['nav-item']}>
                                <a className={styles.navLink} href="#">Contacto</a>
                            </li>
                        </ul>
                    </div>

                </div>
            </nav>
      );
}

export default Navbar;