import React, { useState } from 'react';
import styles from './SeccionPrincipalFichaje.css';

const Input = ({onEnter}) => {

    const [valor, setValor] = useState("")

    const handleKeyDown = (event) => {
        if (event.key === 'Enter') {
            onEnter(valor)
        }
    }
  
    return <input type="text" value={valor} onChange={(e) => setValor(e.target.value)} onKeyDown={handleKeyDown} />
  }

const SeccionPrincipalFichaje = () => {
    function validarEquipo(id) {
        console.log(id);
    }
    
    return (
            <div className={styles.seccion}>
                <label style={{fontWeight:'bolder'}}>Código de tu equipo</label>
                <Input onEnter={validarEquipo}/>
            </div>
    );
}

export default SeccionPrincipalFichaje;
