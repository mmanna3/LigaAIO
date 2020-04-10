import React from 'react';
import Torneos from './Torneos/Torneos';
import Zonas from './Zonas/Zonas';
import Fases from './Fases/Fases';
import Opciones from './Opciones/Opciones';
import Posiciones from './Posiciones/Posiciones';
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
    else if (opcionSeleccionada == "Posiciones")
      return <Posiciones zonaAperturaId={zonaSeleccionada.id} fase={faseSeleccionada}/>
}

export default Menu;
