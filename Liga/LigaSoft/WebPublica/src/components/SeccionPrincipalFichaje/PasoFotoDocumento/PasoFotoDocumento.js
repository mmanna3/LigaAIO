import React, { useState } from 'react';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import Label from '../Label/Label';
import Input from '../Input/Input';
import ImageUploader from '../ImageUploader/ImageUploader'

const PasoFotoDocumento = ({register, estiloDelPaso}) => {

  const [imagen, setImagen] = useState(null)
  const [imagenBase64, setImagenBase64] = useState("")  

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
        
        <img readOnly width="200" src={imagenBase64} />

        <input readOnly name="fotoFrenteDNI" ref={register} style={{display: "none"}} value={imagenBase64} />
        <ImageUploader onChange={onSelectFile} />
        

      </div>
    </div>
    )
}

export default PasoFotoDocumento;
