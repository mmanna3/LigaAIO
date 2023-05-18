import { Link, useParams } from 'react-router-dom';
import { useFetch } from '../common/hooks/useFetch';
import { Noticia } from '../../interfaces/api';
import { Spinner } from '../common/Spinner';

export const NoticiasPage = () => {
  const { data, isFetching } = useFetch<Noticia[]>('noticias');

  if (isFetching) {
    return <Spinner />;
  }

  return (
    <>
      {/* <h2 className='my-6 text-center text-3xl font-bold underline'>Noticias</h2> */}
      <div className='mx-auto flex flex-col items-center gap-4 p-4'>
        {data.map(({ id, titulo, subtitulo, fecha }) => (
          <Card key={id} titulo={titulo} subtitulo={subtitulo} fecha={fecha} id={id}></Card>
        ))}
      </div>
    </>
  );
};

const Card = ({ titulo, subtitulo, fecha, id }: Noticia) => {
  return (
    <Link className='w-[80%] bg-slate-100 p-4 font-arial text-xs shadow-lg' to={`${id}`}>
      <div className='flex gap-2 text-sm'>
        <p className='text-green-600'>{fecha} |</p>
        <p className='font-bold underline'>{titulo}</p>
      </div>
      <p>{subtitulo}</p>
      <p>...</p>
    </Link>
  );
};

export const NoticiaPage = () => {
  const { noticiaId } = useParams();
  console.log(noticiaId);
  const { data, isFetching } = useFetch<Noticia>(`noticia?id=${noticiaId}`);

  if (isFetching) {
    return <Spinner />;
  }

  return (
    <>
      <h2 className='my-6 text-center text-3xl font-bold underline'>{data.titulo}</h2>
      <div className='mx-auto bg-slate-100 py-6 px-10 font-arial text-xs shadow-lg'>
        <div className='flex gap-2 text-sm'>
          <p className='text-green-600'>{data.fecha} |</p>
          <p className='font-bold underline'>{data.titulo}</p>
        </div>
        <p>{data.subtitulo}</p>
        <p dangerouslySetInnerHTML={{ __html: data.cuerpo }}></p>
      </div>
    </>
  );
};
