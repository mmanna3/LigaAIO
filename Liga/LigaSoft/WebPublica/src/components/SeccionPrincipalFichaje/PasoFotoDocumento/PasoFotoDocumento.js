import React, { useState } from 'react';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import Label from '../Label/Label';
import ImageUploader from '../ImageUploader/ImageUploader'
import estilos from './PasoFotoDocumento.css'
import imagenDniFrente from './dniFrente.png';
import imagenDniDorso from './dniDorso.png';

const PasoFotoDocumento = ({register, estiloDelPaso, titulo, errors, name, nombre}) => {

  const imagenDefault = name === 'fotoFrenteDNI' ? imagenDniFrente : imagenDniDorso;
  //manigga del futuro no me juzgues, había poco tiempo y me pagaban poco
  
  const [imagenBase64, setImagenBase64] = useState(imagenDefault)  

  const onSelectFile = e => {
    if (e.target.files && e.target.files.length > 0) {
      const reader = new FileReader();
      reader.addEventListener('load', () => {        
        setImagenBase64(reader.result)
      }
      );
      reader.readAsDataURL(e.target.files[0]);
    }
  };
  
  return (
    <div className={estiloDelPaso}>
      <div className={bootstrap.row}>
        <div className={bootstrap['col-12']+" "+estilos.contenedorDeContenidoCentrado}> 
            <Label texto={titulo} />
        </div>
        
        <div className={estilos.contenedorDeContenidoCentrado}>
          <img readOnly width="200" src={imagenBase64} className={estilos.imagenDNIFrente} />
        </div>                

        <input readOnly name={name} ref={register} style={{display: "none"}} value={imagenBase64} />
        <ImageUploader onChange={onSelectFile} />
        

      </div>
    </div>
    )
}

export default PasoFotoDocumento;
