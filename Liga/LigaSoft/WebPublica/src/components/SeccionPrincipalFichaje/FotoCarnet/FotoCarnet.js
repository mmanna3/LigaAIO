import React, { useState, useCallback, useEffect } from 'react';
import Cropper from 'react-easy-crop'
import Slider from '@material-ui/core/Slider'
import estilos from './FotoCarnet.css';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import obtenerImagenRecortada from './recortarImagen'
import ImageUploader from './ImageUploader'
import persona from './chico.png';
import Label from '../Label/Label';

const FotoCarnet = ({estiloDelPaso, register}) => {

  const [imagen, setImagen] = useState(null)
  const [crop, setCrop] = useState({ x: 0, y: 0 })
  const [zoom, setZoom] = useState(1)
  const [croppedAreaPixels, setCroppedAreaPixels] = useState(null)
  const [imagenRecortada, setImagenRecortada] = useState(persona)

  useEffect(() => {
    window.scrollTo(0, 0);
  }, [imagen])

  const onSelectFile = e => {
    if (e.target.files && e.target.files.length > 0) {
      const reader = new FileReader();
      reader.addEventListener('load', () =>
        setImagen(reader.result)
      );
      reader.readAsDataURL(e.target.files[0]);
    }
  };

  const onCropComplete = useCallback((croppedArea, croppedAreaPixels) => {
    setCroppedAreaPixels(croppedAreaPixels)
  }, [])

  const onAceptarClick = async () => {
    try {
      const croppedImage = await obtenerImagenRecortada(
        imagen,
        croppedAreaPixels
      )
      console.log('imagen bien recortada', { croppedImage })
      setImagenRecortada(croppedImage)
    } catch (e) {
      console.error(e)
    }
    
    setImagen(null);
  }

  const onCancelarClick = () => {
    setImagen(null);
  }

  return (
          <div className={estiloDelPaso}>
            <div className={bootstrap.row}>
              <div className={bootstrap['col-12']+" "+estilos.contenedorDeContenidoCentrado}> 
                  <Label texto={"Tu foto"} />
              </div>                    
                            
              <div className={estilos.contenedorDeContenidoCentrado}>
                <img src={imagenRecortada} alt="Cropped" className={estilos.imagenRecortada} />
              </div>
              
              <ImageUploader value={imagen} onChange={onSelectFile} />
              <input readOnly name="fotoCarnet" ref={register} style={{display: "none"}} value={imagenRecortada} />
              
              {imagen && 
              (
                <div className={estilos.contenedorGeneralDeTodo}>
                  <div className={estilos.cropContainer}>
                    <Cropper
                      image={imagen}
                      crop={crop}                    
                      aspect={4 / 3}
                      onCropChange={setCrop}
                      cropSize={{width: 240, height: 240 }}
                      onCropComplete={onCropComplete}
                      zoom={zoom}
                      onZoomChange={zoom => setZoom(zoom)}
                    />
                  </div>
                  <div className={estilos.sliderContainer}>
                    <Slider
                      value={zoom}
                      min={1}
                      max={3}
                      step={0.1}
                      aria-labelledby="Zoom"
                      onChange={(e, zoom) => setZoom(zoom)}
                    />
                  </div>
                  <div className={estilos.botonesContainer}>
                      <div className={bootstrap.container}>
                        <div className={bootstrap.row}>
                          <div className={bootstrap['offset-2']+" "+bootstrap['col-4']}>
                              <button className={`${bootstrap.btn} ${bootstrap['btn-success']}`} style={{width: "100%"}} onClick={onAceptarClick}>Aceptar</button>
                          </div>
                          <div className={bootstrap['col-4']}>
                              <button className={`${bootstrap.btn} ${bootstrap['btn-danger']}`} style={{width: "100%"}} onClick={onCancelarClick}>Cancelar</button>
                          </div>
                        </div>
                      </div>
                  </div>
                </div>
              )}

            </div>
        </div>
    )
}

export default FotoCarnet;
