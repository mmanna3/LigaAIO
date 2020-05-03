import React from "react";
import styles from "./TablaJornadas.css";

const TablaJornadas = (props) =>{    
     
      return (
        <div className={styles.columnaTabla}>
              <h3 className={styles.tituloBlancoConSombraNegra}>{props.titulo}</h3>
              <table className={styles.tablaPosiciones}>
                <thead>
                <tr>
                  <th className={styles.cabeceraIzquierda}>Pos</th>
                  <th className={styles.cabeceraCentrada}>Esc</th>
                  <th className={styles.cabecera}>Equipo</th>
                  <th className={styles.cabeceraCentrada}>J</th>
                  <th className={styles.cabeceraCentrada}>G</th>
                  <th className={styles.cabeceraCentrada}>E</th>
                  <th className={styles.cabeceraCentrada}>P</th>
                  <th className={styles.cabeceraCentrada}>Np</th>
                  {(props.verGoles ? 
                    (<>
                      <th className={styles.cabeceraCentrada}>Gf</th>
                      <th className={styles.cabeceraCentrada}>Gc</th>
                      <th className={styles.cabeceraCentrada}>Df</th>
                    </>)
                    : (<></>)
                  )}
                  <th className={styles.cabeceraDerecha}>Pts</th>
                </tr>
                </thead>
                <tbody>
                  {props.renglones.map(({ Posicion, Escudo, Equipo, Pj, Pg, Pe, Pp, Np, Gf, Gc, Df, Pts }) => (
                    <tr key={Posicion}>
                      <td className={styles.celdaIzquierda}>{Posicion}</td>
                      <td className={styles.celdaEscudo}><img width="30px" height="auto" alt="Escudo" src={Escudo} /></td>
                      <td className={styles.celda}>{Equipo}</td>
                      <td className={styles.celdaCentrada}>{Pj}</td>
                      <td className={styles.celdaCentrada}>{Pg}</td>
                      <td className={styles.celdaCentrada}>{Pe}</td>
                      <td className={styles.celdaCentrada}>{Pp}</td>
                      <td className={styles.celdaCentrada}>{Np}</td>
                      {(props.verGoles ? 
                        (<>
                          <td className={styles.celdaCentrada}>{Gf}</td>
                          <td className={styles.celdaCentrada}>{Gc}</td>
                          <td className={styles.celdaCentrada}>{Df}</td>
                        </>)
                        : (<></>)
                      )}
                      <td className={styles.celdaDerecha}>{Pts}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
      )
    }

export default TablaJornadas;