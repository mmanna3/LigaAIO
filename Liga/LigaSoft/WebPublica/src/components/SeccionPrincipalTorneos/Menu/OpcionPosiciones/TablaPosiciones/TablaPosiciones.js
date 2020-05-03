import React from "react";
import tabla from './../../../../../assets/styles/tabla.css';

const TablaPosiciones = (props) =>{    
     
      return (
        <div className={tabla.columnaTabla}>
              <h3 className={tabla.tituloBlancoConSombraNegra}>{props.titulo}</h3>
              <table className={tabla.tabla}>
                <thead>
                <tr>
                  <th className={tabla.cabeceraIzquierda}>Pos</th>
                  <th className={tabla.cabeceraCentrada}>Esc</th>
                  <th className={tabla.cabecera}>Equipo</th>
                  <th className={tabla.cabeceraCentrada}>J</th>
                  <th className={tabla.cabeceraCentrada}>G</th>
                  <th className={tabla.cabeceraCentrada}>E</th>
                  <th className={tabla.cabeceraCentrada}>P</th>
                  <th className={tabla.cabeceraCentrada}>Np</th>
                  {(props.verGoles ? 
                    (<>
                      <th className={tabla.cabeceraCentrada}>Gf</th>
                      <th className={tabla.cabeceraCentrada}>Gc</th>
                      <th className={tabla.cabeceraCentrada}>Df</th>
                    </>)
                    : (<></>)
                  )}
                  <th className={tabla.cabeceraDerecha}>Pts</th>
                </tr>
                </thead>
                <tbody>
                  {props.renglones.map(({ Posicion, Escudo, Equipo, Pj, Pg, Pe, Pp, Np, Gf, Gc, Df, Pts }) => (
                    <tr key={Posicion}>
                      <td className={tabla.celdaIzquierda}>{Posicion}</td>
                      <td className={tabla.celdaEscudo}><img width="30px" height="auto" alt="Escudo" src={Escudo} /></td>
                      <td className={tabla.celda}>{Equipo}</td>
                      <td className={tabla.celdaCentrada}>{Pj}</td>
                      <td className={tabla.celdaCentrada}>{Pg}</td>
                      <td className={tabla.celdaCentrada}>{Pe}</td>
                      <td className={tabla.celdaCentrada}>{Pp}</td>
                      <td className={tabla.celdaCentrada}>{Np}</td>
                      {(props.verGoles ? 
                        (<>
                          <td className={tabla.celdaCentrada}>{Gf}</td>
                          <td className={tabla.celdaCentrada}>{Gc}</td>
                          <td className={tabla.celdaCentrada}>{Df}</td>
                        </>)
                        : (<></>)
                      )}
                      <td className={tabla.celdaDerecha}>{Pts}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
      )
    }

export default TablaPosiciones;