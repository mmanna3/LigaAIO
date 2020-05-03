import React from "react";
import tabla from './../../../../../assets/styles/tabla.css';
import styles from './TablaJornadas.css';

const TablaJornadas = (props) =>{    
     
      return (
        <div className={tabla.columnaTabla}>
              <h3 className={tabla.tituloBlancoConSombraNegra}>{props.titulo}</h3>
              <table className={tabla.tablaPosiciones}>
                <thead>
                <tr>
                  <th className={tabla.cabecera}></th>
                  <th className={tabla.cabeceraCentrada}>Esc</th>
                  <th className={tabla.cabecera}>Equipo</th>
                  {props.categorias.map(({Nombre}) => (
                    <th key={Nombre} className={tabla.cabeceraCentrada}>{Nombre}</th>
                  ))}
                  <th className={tabla.cabeceraCentrada}>T.P.</th>
                  <th className={tabla.cabeceraCentrada}>P.J.</th>
                  <th className={tabla.cabeceraDerecha}>V</th>
                </tr>
                </thead>
                <tbody>
                  {props.renglones.map(({JornadaNumero, Escudo, Equipo, ResultadosPorCategorias, PuntosTotales, PartidosJugados, PartidoVerificado }) => (                    
                    <tr key={Equipo} className={JornadaNumero % 2 == 0 ? styles.fondoGris : styles.fondoBlanco}>
                      <td className={tabla.celdaIzquierda}>{JornadaNumero}</td>
                      <td className={tabla.celdaEscudo}><img width="30px" height="auto" alt="Escudo" src={Escudo} /></td>
                      <td className={tabla.celda}>{Equipo}</td>
                      {ResultadosPorCategorias.map(({Orden, Goles}) => (
                        <td key={Orden} className={tabla.celdaCentrada}>{Goles}</td>
                      ))}
                      <td className={tabla.celdaCentrada}>{PuntosTotales}</td>
                      <td className={tabla.celdaCentrada}>{PartidosJugados}</td>
                      <td className={tabla.celdaDerecha}>{PartidoVerificado}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
      )
    }

export default TablaJornadas;