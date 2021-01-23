import React, { useState} from 'react';
import Cropper from 'react-easy-crop'
import Slider from '@material-ui/core/Slider'
import estilos from './FotoCarnet.css';
import bootstrap from "GlobalStyle/bootstrap.min.css";

const FotoCarnet = ({}) => {

  const [imagen, setImagen] = useState(null)
  const [crop, setCrop] = useState({ x: 0, y: 0 })
  const [zoom, setZoom] = useState(1)

  const onSelectFile = e => {
    if (e.target.files && e.target.files.length > 0) {
      const reader = new FileReader();
      reader.addEventListener('load', () =>
        setImagen(reader.result)
      );
      reader.readAsDataURL(e.target.files[0]);
    }
  };

  const onCropComplete = (croppedArea, croppedAreaPixels) => {
    // alert("crop completed")
  }

  return (
          <div>
            <div>
              <input type="file" accept="image/*" onChange={onSelectFile} />
              {imagen && 
              (
                <div className={estilos.contenedorGeneralDeTodo}>
                  <div className={estilos.cropContainer}>
                    <Cropper
                      image={imagen}
                      crop={crop}                    
                      aspect={4 / 3}
                      onCropChange={setCrop}
                      cropSize={{width: 300, height: 300 }}
                      zoom={zoom}
                      onZoomChange={zoom => setZoom(zoom)}
                      onCropComplete={onCropComplete}
                    />
                  </div>
                  <div className={estilos.controls}>
                    <Slider
                      value={zoom}
                      min={1}
                      max={3}
                      step={0.1}
                      aria-labelledby="Zoom"
                      onChange={(e, zoom) => setZoom(zoom)}
                      // classes={{ container: estilos.slider }}
                    />
                  </div>
                </div>
              )}
            </div>
        </div>
    )
}

export default FotoCarnet;
