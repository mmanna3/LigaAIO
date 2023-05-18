import PasoInput from './PasoInput/PasoInput';
import PasoCodigoEquipo from './PasoCodigoEquipo/PasoCodigoEquipo';
import PasoFotoCarnet from './PasoFotoCarnet/PasoFotoCarnet';
import PasoFotoDocumento from './PasoFotoDocumento/PasoFotoDocumento';
import PasoBotonEnviar from './PasoBotonEnviar/PasoBotonEnviar';
import PasoFechaNacimiento from './PasoFechaNacimiento/PasoFechaNacimiento';
import PasoDNI from './PasoDNI/PasoDNI';
import { useForm, FormProvider } from 'react-hook-form';
import MessageBox from '../../common/MessageBox';

interface IProps {
  showLoading: React.Dispatch<React.SetStateAction<boolean>>;
  onSuccess: () => void;
  onError: () => void;
}

const FormularioFichaje = ({ showLoading, onSuccess, onError }: IProps) => {
  const methods = useForm();

  const hacerElPost = async (data: unknown) => {
    showLoading(true);
    fetch('https://www.edefi.com.ar/JugadorAutofichado/autofichaje', {
      method: 'POST',
      mode: 'cors',
      cache: 'no-cache',
      credentials: 'same-origin',
      headers: { 'Content-Type': 'application/json' },
      redirect: 'follow',
      referrerPolicy: 'no-referrer',
      body: JSON.stringify(data),
    })
      .then((res) => res.json())
      .then((res) => {
        console.log('Respuesta', res);
        showLoading(false);
        if (res === 'OK') onSuccess();
        else onError();
      })
      .catch(function (err) {
        console.log('Error del servidor', err);
        showLoading(false);
        onError();
      });
  };

  const onSubmit = (data: any) => {
    hacerElPost(data);
  };

  const huboAlgunError = !(
    Object.keys(methods.formState.errors).length === 0 &&
    methods.formState.errors.constructor === Object
  );

  return (
    <FormProvider {...methods}>
      <div className='flex justify-center font-sans text-slate-100'>
        <div className='max-w-[360px]'>
          <form onSubmit={methods.handleSubmit(onSubmit)}>
            {huboAlgunError && (
              <div className='mb-2'>
                <MessageBox type='error'>
                  ¡Ups! Hubo algún <strong>error</strong>. Revisá tus datos y volvé a enviarlos.
                </MessageBox>
              </div>
            )}

            <PasoCodigoEquipo />

            <PasoInput longMaxima={10} name='nombre' nombre='nombre' titulo='Tu nombre' />

            <PasoInput longMaxima={11} name='apellido' nombre='apellido' titulo='Tu apellido' />

            <PasoDNI />

            <PasoFechaNacimiento />

            <PasoFotoCarnet />

            <PasoFotoDocumento
              titulo='Foto del frente de tu DNI'
              name='fotoDNIFrente'
              nombre='foto de FRENTE del DNI'
            />

            <PasoFotoDocumento
              titulo='Foto de la parte de atrás de tu DNI'
              name='fotoDNIDorso'
              nombre='foto de ATRÁS del DNI'
            />

            <PasoBotonEnviar />
          </form>
        </div>
      </div>
    </FormProvider>
  );
};

export default FormularioFichaje;
