import React from "react";
import styles from "./Footer.css";

const Footer = () => {

    return (
            <div className={styles.footerContainer}>
                <footer>
                    <div className={styles.iconosRedesSociales}>
                        <a href="https://www.facebook.com/ligaedefi/" className={styles.logoFacebook}></a>
                        <a href="https://twitter.com/ligaedefi" className={styles.logoInstagram}></a>
                    </div>
                    <div className={styles.firma}>
                        dev by YKN
                    </div>
                </footer>
            </div>
    );
}

export default Footer;