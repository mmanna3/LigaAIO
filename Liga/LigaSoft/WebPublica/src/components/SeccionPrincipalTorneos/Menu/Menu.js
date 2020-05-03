import React from 'react';
import Torneos from './Torneos/Torneos';
import Zonas from './Zonas/Zonas';
import Fases from './Fases/Fases';
import Opciones from './Opciones/Opciones';
import OpcionPosiciones from './OpcionPosiciones/OpcionPosiciones';
import OpcionJornadas from './OpcionJornadas/OpcionJornadas';
import OpcionClubes from './OpcionClubes/OpcionClubes';
import OpcionFixture from './OpcionFixture/OpcionFixture';
import {useSelector} from 'react-redux';

const Menu = () => {
    const torneoSeleccionado = useSelector(state => state.torneoReducer.torneo);
    const zonaSeleccionada = useSelector(state => state.zonaReducer.zona);
    const faseSeleccionada = useSelector(state => state.faseReducer.fase);
    const opcionSeleccionada = useSelector(state => state.opcionReducer.opcion);    

    if (!torneoSeleccionado)
      return <Torneos />
    else if (torneoSeleccionado && !zonaSeleccionada)
      return <Zonas torneoId={torneoSeleccionado.id} />
    else if (zonaSeleccionada && torneoSeleccionado.formato == "aperturaclausura" && !faseSeleccionada)
      return <Fases />
    else if (!opcionSeleccionada && faseSeleccionada)
      return <Opciones />
    else if (opcionSeleccionada) {

      const zonaSeleccionadaId = getZonaId(zonaSeleccionada, faseSeleccionada);

      if (opcionSeleccionada == "Posiciones")
        return <OpcionPosiciones zonaId={zonaSeleccionadaId} esAnual={faseSeleccionada == 'Anual'}/>
      else if (opcionSeleccionada == "Jornadas")
        return <OpcionJornadas zonaId={zonaSeleccionadaId}/>
      else if (opcionSeleccionada == "Clubes")
        return <OpcionClubes zonaId={zonaSeleccionadaId}/>
      else if (opcionSeleccionada == "Fixture")
        return <OpcionFixture zonaId={zonaSeleccionadaId}/>
    }

    function getZonaId(zona, fase) {
      switch (fase) {
        case 'Apertura':
          return zona.zonaAperturaId;
        case 'Clausura':
          return zona.zonaClausuraId;
        case 'Anual':
          return zona.zonaAperturaId;
        case 'Relampago':
          return zona.zonaRelampagoId;
      }
    }
}

export default Menu;
