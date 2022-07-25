import React from 'react';
import estilos from './Estilos.css';
import bootstrap from './../../../assets/styles/bootstrap.min.css'
import carnet from './carnet.jpg'

const SegundaSeccion = () => {

    return (

        <div className={estilos.franjaAzul}>
            <div className={bootstrap.container}>
                <div className={bootstrap.row}>
                    <div className={bootstrap["col-12"]+" "+bootstrap["d-lg-none"]}>
                        <div className={estilos.texto}>
                            <h2>Fichaje online</h2>
                            <h4>¡sin salir de tu casa!</h4>
                            <p>Ingresando tus datos podrás ficharte para jugar en EDeFI.</p>
                            <p>Solo debés pedirle el código de equipo a tu delegado.</p>
                            <p>Luego, la administración de la Liga se estará contactando con tu club.</p>
                        </div>
                    </div>
                    <div className={bootstrap["col-12"]+' '+bootstrap["col-lg-7"]}>
                        <div className={estilos.contenedorImagen}>
                            <img src={carnet} className={estilos.carnet} />
                        </div>
                    </div>
                    <div className={bootstrap["col-lg-5"]+" "+bootstrap["d-none"]+" "+bootstrap["d-lg-block"]}>
                        <div className={estilos.texto}>
                            <h2>Fichaje online</h2>
                            <h4>¡sin salir de tu casa!</h4>
                            <p>Ingresando tus datos podrás ficharte para jugar en EDeFI.</p>
                            <p>Solo debés pedirle el código de equipo a tu delegado.</p>
                            <p>Luego, la administración de la Liga se estará contactando con tu club.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
)
}

export default SegundaSeccion;
