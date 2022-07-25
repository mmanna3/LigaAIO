import React, { useState } from 'react';
import PrimeraSeccion from './primeraSeccion/primeraSeccion';
import SegundaSeccion from './segundaSeccion/segundaSeccion';
import TerceraSeccion from './terceraSeccion/terceraSeccion';

const NuevoHome = () => {

    return (
    <div>
        <PrimeraSeccion/>
        <SegundaSeccion/>
        <TerceraSeccion/>
    </div>)
}

export default NuevoHome;
