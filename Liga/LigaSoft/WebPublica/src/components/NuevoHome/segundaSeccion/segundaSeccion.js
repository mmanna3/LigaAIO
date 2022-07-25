import React from 'react';
import estilos from './Estilos.css';
import bootstrap from './../../../assets/styles/bootstrap.min.css'
import Copa from './../../../assets/imgs/copa.svg'
import Podio from './../../../assets/imgs/podio.svg'

const SegundaSeccion = () => {

    return (

        <div className={estilos.torneosYCopasContenedor}>
            <div className={bootstrap.container}>
                <div className={bootstrap.row}>
                    <div className={bootstrap["col-6"]}>
                        <div className={estilos.tipoLigaContenedor}>
                            <img className={estilos.verde} src={Copa} />
                        </div>
                        <div className={estilos.leyenda}>
                            Torneos
                        </div>                        
                    </div>
                    <div className={bootstrap["col-6"]}>
                        <div className={estilos.tipoLigaContenedor}>
                            <img className={estilos.verde} src={Podio} />
                        </div>
                        <div className={estilos.leyenda}>
                            Copas
                        </div>
                    </div>
                </div>
            </div>
        </div>
)
}

export default SegundaSeccion;
