import React from "react";
import styles from './Spinner.css';

const Spinner = () => {
      return (
        <div className={styles.container}>
          <div className={styles['lds-spinner']}><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>
        </div>
      )
}

export default Spinner;