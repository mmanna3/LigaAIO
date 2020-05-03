import React from "react";
import {fetchDataAndRenderResponse} from "Utils/hooks";
import TablaFixture from "./TablaFixture/TablaFixture";
import styles from "./OpcionFixture.css";

const OpcionFixture = (props) =>{    

  const render = (data) => {

      return (
        <div className={styles.rowTablas}>
          {data.Fechas.map(({ DiaDeLaFecha, Titulo, LocalVisitante }) => (
            <TablaFixture key={DiaDeLaFecha} diaDeLaFecha={DiaDeLaFecha} titulo={Titulo} renglones={LocalVisitante} />
          ))}
        </div>
      )
    }    

    return fetchDataAndRenderResponse("/publico/fixture?zonaId="+props.zonaId, render);
}

export default OpcionFixture;