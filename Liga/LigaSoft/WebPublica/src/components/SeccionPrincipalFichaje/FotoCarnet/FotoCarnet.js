import React, { useState} from 'react';
import Cropper from 'react-easy-crop'
import styles from './FotoCarnet.css';
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
    alert("crop completed")
  }

  return (
          <div>
            <div>
              <input type="file" accept="image/*" onChange={onSelectFile} />
              {imagen && 
              (
                <div className="crop-container">
                  <Cropper
                    image={imagen}
                    crop={crop}                    
                    aspect={4 / 3}
                    onCropChange={setCrop}
                    cropSize={{width: 300, height: 300 }}
                    // zoom={zoom}
                    // onZoomChange={zoom => setZoom(zoom)}
                    onCropComplete={onCropComplete}
                  />
                </div>
              )}
            </div>
        </div>
    )
}

export default FotoCarnet;
