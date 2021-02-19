import React from "react";
import tabla from './../../../../../assets/styles/tabla.css';
import styles from './TablaSanciones.css'

const TablaSanciones = (props) =>{    
     
      return (
        <div className={tabla.columnaTabla}>
              <h3 className={tabla.tituloBlancoConSombraNegra}>{props.titulo}</h3>
              <table className={styles.tabla}>
                <thead>
                <tr>
                  <th className={tabla.cabeceraIzquierda}>{props.titulo}</th>
                  <th className={tabla.cabecera}></th>
                  <th className={tabla.cabecera}></th>
                  <th className={tabla.cabecera}></th>
                  <th className={tabla.cabeceraDerecha}>{props.diaDeLaFecha}</th>
                </tr>
                </thead>
                <tbody>
                  {props.renglones.map(({EscudoLocal, Local, Visitante, EscudoVisitante}) => (
                    <tr key={Local}>
                      <td className={tabla.celdaEscudo}><img width="30px" height="auto" alt="EscudoLocal" src={EscudoLocal} /></td>
                      <td className={tabla.celda}>{Local}</td>
                      <td className={tabla.celdaCentrada}>vs.</td>
                      <td className={tabla.celdaAlineadaALaDerecha}>{Visitante}</td>
                      <td className={tabla.celdaEscudo}><img width="30px" height="auto" alt="EscudoVisitante" src={EscudoVisitante} /></td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
      )
    }

export default TablaSanciones;