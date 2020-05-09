import React from "react";
import {fetchDataAndRenderResponse} from "Utils/hooks";
import styles from "./OpcionClubes.css";
import tabla from './../../../../assets/styles/tabla.css';

const OpcionClubes = (props) =>{    

  const render = (data) => {

      return (
        <div className={styles.rowTablas}>
          <div className={styles.columnaClubes}>
              <table className={tabla.tabla}>
                <thead>
                <tr>
                  <th className={styles.cabeceraIzquierda}></th>
                  <th className={styles.cabeceraTexto}>Equipo</th>
                  <th className={styles.cabeceraTexto}>Localidad</th>
                  <th className={styles.cabeceraTexto}>Dirección</th>
                  <th className={tabla.cabeceraDerecha}>Techo</th>
                </tr>
                </thead>
                <tbody>
                  {data.Renglones.map(({Equipo, Escudo, Direccion, Localidad, TechoDescripcion}) => (
                    <tr key={Equipo}>
                      <td className={tabla.celdaEscudo}><img width="30px" height="auto" alt="Escudo" src={Escudo} /></td>                  
                      <td className={tabla.celda}>{Equipo}</td>
                      <td className={tabla.celda}>{Localidad}</td>
                      <td className={tabla.celda}>{Direccion}</td>
                      <td className={tabla.celdaDerecha}>{TechoDescripcion}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
        </div>
      )
    }    

    return fetchDataAndRenderResponse("/publico/clubes?zonaId="+props.zonaId, render);
}

export default OpcionClubes;