import React from "react";
import styles from "./TablaJornadas.css";

const TablaJornadas = (props) =>{    
     
      return (
        <div className={styles.columnaTabla}>
              <h3 className={styles.tituloBlancoConSombraNegra}>{props.titulo}</h3>
              <table className={styles.tablaPosiciones}>
                <thead>
                <tr>
                  <th className={styles.cabecera}></th>
                  <th className={styles.cabeceraCentrada}>Esc</th>
                  <th className={styles.cabecera}>Equipo</th>
                  {props.categorias.map(({Nombre}) => (
                    <th key={Nombre} className={styles.cabeceraCentrada}>{Nombre}</th>
                  ))}
                  <th className={styles.cabeceraCentrada}>T.P.</th>
                  <th className={styles.cabeceraCentrada}>P.J.</th>
                  <th className={styles.cabeceraDerecha}>V</th>
                </tr>
                </thead>
                <tbody>
                  {props.renglones.map(({JornadaNumero, Escudo, Equipo, ResultadosPorCategorias, PuntosTotales, PartidosJugados, PartidoVerificado }) => (                    
                    <tr key={Equipo}>
                      <td className={styles.celdaIzquierda}>{JornadaNumero}</td>
                      <td className={styles.celdaEscudo}><img width="30px" height="auto" alt="Escudo" src={Escudo} /></td>
                      <td className={styles.celda}>{Equipo}</td>
                      {ResultadosPorCategorias.map(({Orden, Goles}) => (
                        <td key={Orden} className={styles.celdaCentrada}>{Goles}</td>
                      ))}
                      <td className={styles.celdaCentrada}>{PuntosTotales}</td>
                      <td className={styles.celdaCentrada}>{PartidosJugados}</td>
                      <td className={styles.celdaDerecha}>{PartidoVerificado}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
      )
    }

export default TablaJornadas;