import React from "react";
import {fetchDataAndRenderResponse} from "Utils/hooks";
import TablaPosiciones from "./TablaPosiciones/TablaPosiciones";
import styles from "./OpcionPosiciones.css";

const OpcionPosiciones = (props) =>{    

  const render = (data) => {
      const verGoles = data.VerGoles;
      
      return (
        <div className={styles.rowTablas}>
          {data.TablasPorCategoria.map(({ Categoria, Renglones }) => (
            <TablaPosiciones key={Categoria} titulo={Categoria} renglones={Renglones} verGoles={verGoles}/>
          ))}
          <TablaPosiciones titulo="General" renglones={data.TablaGeneral.Renglones} verGoles={verGoles}/>
        </div>
      )
    }    

    if (!props.esAnual)
      return fetchDataAndRenderResponse("/publico/posiciones?zonaId="+props.zonaId, render);
    else
      return fetchDataAndRenderResponse("/publico/posicionesAnual?zonaAperturaId="+props.zonaId, render);
}

export default OpcionPosiciones;