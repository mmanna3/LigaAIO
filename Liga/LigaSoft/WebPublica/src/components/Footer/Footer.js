import React from "react";
import styles from "./Footer.css";
import bootstrap from "GlobalStyle/bootstrap.min.css";

const Footer = () => {


    return (
            <div className={styles.footerContainer}>
                <footer className={styles.pageFooter}>
                            <a href="https://www.facebook.com/ligaedefi/" className={styles.logoFacebook}></a>
                            <a href="https://twitter.com/ligaedefi" className={styles.logoTwitter}></a>
                </footer>
            </div>
    );
}

export default Footer;