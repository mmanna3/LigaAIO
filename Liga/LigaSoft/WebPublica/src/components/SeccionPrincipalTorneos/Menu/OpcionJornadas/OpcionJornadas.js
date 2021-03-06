import React from "react";
import {fetchDataAndRenderResponse} from "Utils/hooks";
import TablaJornadas from "./TablaJornadas/TablaJornadas";
import styles from "./OpcionJornadas.css";

const OpcionJornadas = (props) =>{    

  const render = (data) => {
    const categorias = data.Categorias;

      return (
        <div className={styles.rowTablas}>
          {data.JornadasPorFecha.map(({ FechaNumero, Renglones }) => (
            <TablaJornadas key={FechaNumero} titulo={FechaNumero} renglones={Renglones} categorias={categorias}/>
          ))}
        </div>
      )
    }    

    return fetchDataAndRenderResponse("/publico/jornadas?zonaId="+props.zonaId, render);
}

export default OpcionJornadas;