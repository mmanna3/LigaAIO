import React from "react";
import {fetchDataAndRenderResponse} from "Utils/hooks";
import TablaSanciones from "./TablaSanciones/TablaSanciones";
import styles from "./OpcionSanciones.css";

const OpcionSanciones = (props) =>{

  const render = (data) => {

      return (
        <div className={styles.rowTablas}>
          {data.Fechas.map(({ DiaDeLaFecha, Titulo, LocalVisitante }) => (
            <TablaSanciones key={DiaDeLaFecha} diaDeLaFecha={DiaDeLaFecha} titulo={Titulo} renglones={LocalVisitante} />
          ))}
        </div>
      )
    }    

    return fetchDataAndRenderResponse("/publico/sanciones?zonaId="+props.zonaId, render);
}

export default OpcionSanciones;