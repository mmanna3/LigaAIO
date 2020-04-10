import React from "react";
import {fetchDataAndRenderResponse} from "Utils/hooks";
import TablaPosiciones from "./TablaPosiciones/TablaPosiciones";
import styles from "./Posiciones.css";

const Posiciones = (props) =>{    

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

    return fetchDataAndRenderResponse("/publico/posiciones?zonaAperturaId="+props.zonaAperturaId+"&fase="+props.fase, render);
}

export default Posiciones;