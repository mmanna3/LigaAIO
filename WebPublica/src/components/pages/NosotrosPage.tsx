import { ReactNode } from 'react';
import IMG_NOSOTROS_TITLE from '../../assets/images/mobile/titles/nosotros-title.avif';
import { Title } from '../common/Title';

interface Props {
  children: ReactNode;
}
const Text = ({ children }: Props) => {
  return <p className='mb-6 font-arial'>{children}</p>;
};

export const NosotrosPage = () => {
  return (
    <>
      <Title img={IMG_NOSOTROS_TITLE} alt='Nosotros' />
      <div className='mx-6 md:mx-20'>
        <h2 className='mb-6 mt-12 text-title-darkGreen md:text-xl lg:text-3xl '>Quiénes somos</h2>
        <div className=' rounded-md bg-gradient-to-tl from-title-darkGreen to-slate-100 p-6 text-justify text-sm opacity-70 sm:p-8 md:text-lg lg:p-14 lg:text-2xl '>
          <div className='mb-10'>
            <Text>
              Nuestra Liga es dirigida por Profesores de Educación Física, donde además de ser
              docentes en el ámbito escolar tenemos una amplia trayectoria y recorrido por el ámbito
              del fútbol, desde profes de baby hasta directores técnicos y preparadores físicos en
              fútbol 11 de diferentes clubes, trabajando hace más de 15 años con chicos de todas las
              edades, desde niños de 3 años hasta jugadores profesionales.
            </Text>
            <p className='mb-6 font-arial'></p>
            <p className='mb-6 font-arial'>
              Nuestra experiencia y formación nos dieron el pie para comenzar esta nueva etapa como
              organizadores de diferentes torneos, nucleando hoy en día gran cantidad de
              Instituciones y promoviendo el deporte y la actividad física a miles de chicos.
            </p>
            <p className='mb-6 font-arial'>
              Iniciamos éste proyecto en el año 2014, realizando encuentros deportivos entre ocho
              escuelas de fútbol con profes conocidos, jugando los días sábados por la mañana de
              manera amistosa, inculcando valores a través de este hermoso deporte que es el fútbol.
              En el año 2015 se duplicó la cantidad de equipos, lo que nos llevó a darle una
              formalidad al torneo, estableciendo un reglamento, otorgando un carnet identificatorio
              para cada jugador, publicando tabla de posiciones y resultados, y entregando un trofeo
              para cada chico.
            </p>
            <p className='mb-6 font-arial'>
              Años siguientes, mejoramos nuestra estructura y organización, incorporando nuevos
              torneos y zonas de baby fútbol, tanto por la mañana como por la tarde; formamos
              nuestro propio staff de árbitros, brindándoles cada año un nuevo curso para aquellos
              que recién se inician y capacitaciones para los que continúan la carrera; impulsamos
              una nueva disciplina, como es el Fútsal, tanto masculino como femenino, lo que
              permitió a muchos clubes seguir educando a través del deporte a esos chicos que
              finalizaban la etapa del baby fútbol y tenían que irse del club; y brindamos mejor
              calidad de atención a cada uno de los clubes.
            </p>
            <p className='mb-6 font-arial'>
              Actualmente la liga cuenta con más de 100 equipos donde participan anualmente miles de
              chicos y chicas, de todas las edades, en diferentes torneos, proyectando este lindo
              deporte desde la edad de la niñez, comenzando por el baby fútbol, pasando por la
              adolescencia, etapa final del baby fútbol y comienzo del Fútsal, hasta la adultez,
              finalizando la carrera como jugador de Fútsal.
            </p>
          </div>
          <div className='mb-10'>
            <h2 className='my-6  text-title-darkGreen md:text-xl lg:text-3xl '>Misión</h2>
            <p className='mb-6 font-arial'>
              Fomentar, promover, desarollar e impulsar la actividad deportiva a través de la
              organización de torneos, en donde los niños y jóvenes deportistas tengan una actividad
              organizada de calidad y con base en los principios del juego limpio, que fomenten la
              salud y la cultura del deporte de las presentes y nuevas generaciones.{' '}
            </p>
          </div>
          <div className='mb-10'>
            <h2 className='my-6 text-title-darkGreen md:text-xl lg:text-3xl '>Visión</h2>
            <p className='mb-6 font-arial'>
              Ser reconocidos como una Liga líder en la organización de eventos deportivos que
              satisfaga las necesidades de las Instituciones y brinde un espacio de oportunidades
              para el crecimiento integral de nuestros deportistas, padres de familias,
              entrenadores, árbitros y directivos fomentando los principios y valores éticos bajo la
              filosofía y prestigio de LIGA EDEFI.
            </p>
          </div>
          <div className='mb-10'>
            <h2 className='my-6 text-title-darkGreen md:text-xl lg:text-3xl '>Valores</h2>
            <p className='mb-6 font-arial'>
              Empatía, Resolución, Competitividad, Responsabilidad, Honestidad, Amistad,
              Participación, Respeto, Humildad, Compromiso y Decisión.
            </p>
          </div>
        </div>
      </div>
    </>
  );
};
