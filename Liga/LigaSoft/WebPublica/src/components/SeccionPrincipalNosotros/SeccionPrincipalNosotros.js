import React from 'react';
import styles from './SeccionPrincipalNosotros.css';

const SeccionPrincipalNosotros = () => {
    return (
            <div className={styles.seccion}>
                <h3 className={styles.titulo}>Quiénes somos</h3>
                <div className={styles.contenido}>
                    <p className={styles.parrafo}>
                        Nuestra Liga es dirigida por Profesores de Educación Física, donde además de ser docentes en el ámbito escolar tenemos una amplia trayectoria y recorrido por el ámbito del futbol, desde profes de baby hasta director técnico y preparadores físicos en futbol 11 de diferentes clubes, trabajando hace más de 15 años con chicos de todas las edades, desde niños de 3 años hasta jugadores profesionales.
                    </p>
                    <p className={styles.parrafo}>
                        Nuestra experiencia y formación constante nos dio el pie para comenzar esta nueva etapa como organizadores de diferentes torneos, nucleando hoy en día gran cantidad de instituciones y promoviendo el deporte y la actividad física a miles de chicos.
                    </p>
                    <p className={styles.parrafo}>
                        Iniciamos este proyecto en el año 2014, realizando encuentros deportivos entre ocho escuelas de futbol con profes conocidos, jugando los días sábados por la mañana de manera amistosa, inculcando valores a través de este hermoso deporte que es el futbol. En el año 2015 se duplico la cantidad de equipos, lo que nos llevó a darle una formalidad al torneo, estableciendo un reglamento, otorgando un carnet identificatorio para cada jugador, publicando tabla de posiciones y resultados, y entregando un trofeo para cada chico.
                    </p>
                    <p className={styles.parrafo}>
                        Años siguientes, mejoramos nuestra estructura y organización, incorporando nuevos torneos y zonas de baby futbol, tanto por la mañana como por la tarde; formamos nuestro propio staff de árbitros, brindándoles cada año un nuevo curso para aquellos que recién se inician y  capacitaciones para los que continúan la carrera; impulsamos una nueva disciplina, como es el Futsal, tanto masculino como femenino, lo que le permitió a muchos clubes seguir educando a través del deporte a esos chicos que finalizaban la etapa del baby futbol y tenían que irse del club; y brindamos mejor calidad de atención a cada uno de los clubes.
                    </p>
                    <p className={styles.parrafo}>
                        Actualmente la liga cuenta con más de 100 equipos donde participan anualmente miles de chicos y chicas, de todas las edades, en diferentes torneos, proyectando este lindo deporte desde la edad de la niñez, comenzando con el baby futbol, pasando por la adolescencia, etapa final del baby futbol y comienzo del Futsal, hasta la adultez, finalizando la carrera como jugador de Futsal.
                    </p>
                </div>
                <h3 className={styles.titulo}>Misión</h3>
                <div className={styles.contenido}>
                    <p className={styles.parrafo}>
                        Fomentar, promover, desarrollar e impulsar la actividad deportiva a través de la organización de torneos, en donde los niños y jóvenes deportistas tengan una actividad organizada de calidad y con base en los principios del juego limpio, que fomenten la salud y la cultura del deporte de las presentes y nuevas generaciones.
                    </p>
                </div>
                <h3 className={styles.titulo}>Visión</h3>
                <div className={styles.contenido}>
                    <p className={styles.parrafo}>
                        Ser reconocidos como una liga líder en la organización de eventos deportivos que satisfaga las necesidades de las instituciones y brinde un espacio de oportunidades para el crecimiento integral de nuestros deportistas, padres de familia, entrenadores, árbitros, directivos. Fomentando los principios y valores éticos bajo la filosofía y prestigio de LIGA EDEFI.
                    </p>
                </div>
                <h3 className={styles.titulo}>Valores</h3>
                <div className={styles.contenido}>
                    <p className={styles.parrafo}>
                        Empatía, Resolución, Competitividad, Responsabilidad, Honestidad, Amistad, Participación, Respeto, Humildad, Compromiso y Decisión.
                    </p>
                </div>
            </div>
    );
}

export default SeccionPrincipalNosotros;
