import { useRouteError } from 'react-router-dom';

interface ReactRouterError {
  statusText: string;
  message: string;
}

export const ErrorNotFound = () => {
  const error = useRouteError() as ReactRouterError;
  console.error(error);

  return (
    <div id='error-page'>
      <h1>Uh!</h1>
      <p>Esta ruta no existe.</p>
      <p>
        <i>{error.statusText || error.message}</i>
      </p>
    </div>
  );
};
