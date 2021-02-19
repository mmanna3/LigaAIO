import React from "react";
import {fetchDataAndRenderResponse} from "Utils/hooks";
// import TablaSanciones from "./TablaSanciones/TablaSanciones";
import tabla from './../../../../assets/styles/tabla.css';
import styles from "./OpcionSanciones.css";

const OpcionSanciones = (props) =>{

  const render = (data) => {

      return (
        
              <table className={styles.tabla}>
                <thead>
                <tr>
                  <th className={tabla.cabeceraIzquierda}>Día</th>
                  <th className={tabla.cabeceraCentrada}>Fecha</th>
                  <th className={tabla.cabeceraCentrada}>Categoría</th>
                  <th className={tabla.cabeceraCentrada}>Local</th>
                  <th className={tabla.cabeceraCentrada}>Visitante</th>
                  <th className={tabla.cabeceraCentrada}>Sanción</th>
                  <th className={tabla.cabeceraDerecha}>Fechas adeudadas</th>
                </tr>
                </thead>
                <tbody>
                  {data.map(({dia, fecha, categoria, local, visitante, sancion, fechasQueAdeuda}) => (
                    <tr key={dia}>                      
                      <td className={tabla.celdaIzquierda}>{dia}</td>
                      <td className={tabla.celdaCentrada}>{fecha}</td>
                      <td className={tabla.celdaCentrada}>{categoria}</td>
                      <td className={tabla.celdaCentrada}>{local}</td>
                      <td className={tabla.celdaCentrada}>{visitante}</td>
                      <td className={tabla.celdaCentrada}>{sancion}</td>
                      <td className={tabla.celdaDerecha}>{fechasQueAdeuda}</td>
                    </tr>
                  ))}
                </tbody>
              </table>

      )
    }    

    return fetchDataAndRenderResponse("/publico/sanciones?zonaId="+props.zonaId, render);
}

export default OpcionSanciones;