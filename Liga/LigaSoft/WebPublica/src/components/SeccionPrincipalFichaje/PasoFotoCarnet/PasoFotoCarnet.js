import React, { useState, useCallback, useEffect } from 'react';
import Cropper from 'react-easy-crop'
import Slider from '@material-ui/core/Slider'
import estilos from './PasoFotoCarnet.css';
import bootstrap from "GlobalStyle/bootstrap.min.css";
import obtenerImagenRecortada from './recortarImagen'
import ImageUploader from '../ImageUploader/ImageUploader'
import persona from './chico.jpg';
import Label from '../Label/Label';
import Error from '../Error/Error';

const PasoFotoCarnet = ({estiloDelPaso, register, errors}) => {

  const [imagen, setImagen] = useState(null)
  const [crop, setCrop] = useState({ x: 0, y: 0 })
  const [zoom, setZoom] = useState(1)
  const [croppedAreaPixels, setCroppedAreaPixels] = useState(null)
  const [imagenRecortada, setImagenRecortada] = useState(persona)
  const [posicionDelScrollAntesDeAbrirRecortador, setPosicionDelScrollAntesDeAbrirRecortador] = useState()

  useEffect(() => {
    alAbrirRecortadorScrolearHastaArribaYAlCerrarloVolver(imagen)
  }, [imagen])

  const alAbrirRecortadorScrolearHastaArribaYAlCerrarloVolver = (imagen) => {
    if (window.pageYOffset != 0)
      setPosicionDelScrollAntesDeAbrirRecortador(window.pageYOffset);

    if (imagen != null)
      window.scrollTo(0, 0)
    else
      window.scrollTo(0, posicionDelScrollAntesDeAbrirRecortador);
  }

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
                  <Label texto={"Tu foto"} subtitulo="Tiene que tener fondo liso" centrado={true} />
              </div>                    
                            
              <div className={estilos.contenedorDeContenidoCentrado}>
                <img src={imagenRecortada} alt="Cropped" className={estilos.imagenRecortada} />
              </div>
              
              <ImageUploader value={imagen} onChange={onSelectFile} />
              <input  readOnly 
                      name="fotoCarnet" 
                      ref={register({validate: value => value !== persona || '¡Ups! Te olvidaste tu foto.'})}
                      style={{display: "none"}} value={imagenRecortada} 
              />
              <Error name={"fotoCarnet"} errors={errors} nombre="foto"/>

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
                              <button type="button" className={`${bootstrap.btn} ${bootstrap['btn-success']}`} style={{width: "100%"}} onClick={onAceptarClick}>Aceptar</button>
                          </div>
                          <div className={bootstrap['col-4']}>
                              <button type="button" className={`${bootstrap.btn} ${bootstrap['btn-danger']}`} style={{width: "100%"}} onClick={onCancelarClick}>Cancelar</button>
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

export default PasoFotoCarnet;
