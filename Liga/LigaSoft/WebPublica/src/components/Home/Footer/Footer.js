import React from "react";
import styles from "./Footer.css";

const Footer = () => {

    return (
            <div className={styles.footerContainer}>
                <footer>
                    <div className={styles.iconosRedesSociales}>
                        <a href="https://www.facebook.com/ligaedefi/" className={styles.logoFacebook}></a>
                        <a href="https://www.instagram.com/liga_edefi/" className={styles.logoInstagram}></a>
                    </div>
                    <div className={styles.firma}>
                        <span>dev by&nbsp;</span><span><a href="mailto:ykn.software.com@gmail.com">YKN</a></span>
                    </div>
                </footer>
            </div>
    );
}

export default Footer;