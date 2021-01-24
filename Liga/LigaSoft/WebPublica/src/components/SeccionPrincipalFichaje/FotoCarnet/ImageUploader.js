import React from 'react';
import estilos from './FotoCarnet.css';
import bootstrap from "GlobalStyle/bootstrap.min.css";

const ImageUploader = ({ value, onChange, ...etc }) => (
  <div style={{width: '100%'}}>
    <label style={{width: '100%'}}>
      <div className={estilos.contenedorDeContenidoCentrado}>
        <span className={`${bootstrap.btn} ${bootstrap['btn-danger']} ${estilos.botonImageUploader}`}>SUBILA</span>
      </div>
      <input
        {...etc}
        style={{ display: "none" }}
        type="file"
        accept="image/*"
        onChange={onChange}
      />
    </label>
  </div>
);

export default ImageUploader;