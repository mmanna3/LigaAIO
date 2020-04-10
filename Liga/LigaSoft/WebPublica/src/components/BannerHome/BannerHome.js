import React from "react";
import styles from "./BannerHome.css";
import bootstrap from "GlobalStyle/bootstrap.min.css";
import colors from "GlobalStyle/colors.css";
import {colorToBackgroundGradientClass} from "Utils/helpers";
import {actualizarSeccionPrincipal} from 'Store/seccion-principal/action';
import {useDispatch} from 'react-redux';

const BannerHome = (props) => {

  const dispatch = useDispatch();
  const backgroundColorClass = colorToBackgroundGradientClass(props.color);

  return (
          <div className={styles.rowWithMarginTop2em}>
            <div className={bootstrap['col-md-12']}>
              <div onClick={() => dispatch(actualizarSeccionPrincipal(props.titulo))} className={styles.banner+' '+colors[backgroundColorClass]}>
                <h2 className={styles.texto}>{props.titulo}</h2>
              </div>
            </div>
          </div>
      );
}

export default BannerHome;