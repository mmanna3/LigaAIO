import React, { useState } from 'react';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import Label from '../Label/Label';
import Input from '../Input/Input';
import Estilos from './PasoCodigoEquipo.css';
import Error from '../Error/Error'

const PasoCodigoEquipo = ({register, errors, estiloDelPaso}) => {

  const [codigoEquipo, setCodigoEquipo] = useState()
  const [codigoEquipoEsValido, setCodigoEquipoEsValido] = useState(null)
  const [nombreEquipo, setNombreEquipo] = useState("")
  const [yaValidoCodigoEquipo, setYaValidoCodigoEquipo] = useState(false)
  
  const onCodigoEquipoChange = (id) => {
    setCodigoEquipo(id)
  }

  const onValidarClick = () => {
      fetch(`/publico/obtenerNombreDelEquipo?equipoId=${codigoEquipo}`)
          .then(response => response.json())
          .then(data => {setNombreEquipo(data); setCodigoEquipoEsValido(true); setYaValidoCodigoEquipo(true)})
          .catch(() => {setCodigoEquipoEsValido(false); setYaValidoCodigoEquipo(true)})
  }   

  return (
    <div className={`${estiloDelPaso}`}>
      <div className={`${bootstrap['form-group']} ${bootstrap.row}`}>
          <div className={bootstrap['col-12']}> 
              <Label texto={"Código de tu equipo"} subtitulo="Pedíselo a tu delegado" />
          </div>
          <div className={bootstrap['col-6']}> 
              <Input
                  onChange={onCodigoEquipoChange}
                  type="number"
                  register={register} 
                  name="codigoEquipo"
              />
          </div>
          <div className={bootstrap['col-6']}> 
              <button type="button" className={`${bootstrap.btn} ${bootstrap['btn-success']}`} style={{width: "100%"}} onClick={onValidarClick}>Validar</button>
          </div>
          {
              yaValidoCodigoEquipo &&
              (codigoEquipoEsValido ?
                  <div className={bootstrap['col-12']}>
                      <div className={`${bootstrap['alert']} ${bootstrap['alert-success']} ${Estilos.alertaValidacionEquipo}`}>
                          Tu equipo es <strong>{nombreEquipo}</strong>
                      </div>
                  </div> :
                  <div className={bootstrap['col-12']}>
                      <div className={`${bootstrap['alert']} ${bootstrap['alert-danger']} ${Estilos.alertaValidacionEquipo}`}>
                          El código es incorrecto
                      </div>
                  </div>
              )
          }
          <Error name="codigoEquipo" nombre="código" errors={errors} />
      </div>
  </div>
    )
}

export default PasoCodigoEquipo;
