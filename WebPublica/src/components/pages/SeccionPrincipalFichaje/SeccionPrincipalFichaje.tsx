import { useState } from 'react';
import FormularioFichaje from './FormularioFichaje';
import MessageBox from '../../common/MessageBox';

const SeccionPrincipalFichaje = () => {
  const [mensajeExitoVisible, mostrarMensajeExito] = useState(false);
  const [mensajeErrorServidorVisible, mostrarMensajeErrorServidor] = useState(false);
  const [spinnerVisible, mostrarSpinner] = useState(false);

  const estaLaSeccionHabilitada = () => {
    const hoy = new Date();
    const diaDeHoy = hoy.getDay();
    const horaActual = hoy.getHours();
    if (diaDeHoy == 6 || diaDeHoy == 0 || diaDeHoy == 5 || (diaDeHoy == 4 && horaActual >= 20))
      return false;
    return true;
  };

  // if (!estaLaSeccionHabilitada())
  if (false)
    return (
      <MessageBox type='info' large>
        El fichaje está deshabilitado.
      </MessageBox>
    );
  else if (mensajeExitoVisible)
    return (
      <MessageBox type='info' large>
        <>
          ¡Tus datos se enviaron correctamente! Gracias por ficharte.
          <div className='mt-6'>
            <button
              onClick={() => mostrarMensajeExito(false)}
              className='rounded-lg bg-green-700 py-3 px-3 text-center text-white'
            >
              Fichar otro jugador
            </button>
          </div>
        </>
      </MessageBox>
    );
  else if (mensajeErrorServidorVisible)
    return (
      <MessageBox type='error' large>
        ¡Ups! Hubo un <strong>error</strong>. Volvé a intentar más tarde.
      </MessageBox>
    );
  else if (spinnerVisible) return <>Ponele que soy un spinner</>;
  else
    return (
      <FormularioFichaje
        showLoading={mostrarSpinner}
        onSuccess={() => mostrarMensajeExito(true)}
        onError={() => mostrarMensajeErrorServidor(true)}
      />
    );
};

export default SeccionPrincipalFichaje;
