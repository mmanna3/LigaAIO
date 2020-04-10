import { COLOR } from "./consts";

export function colorToBackgroundGradientClass(color) {
    let backgroundGradientClass = "";
    if (color === COLOR.ROJO)
      backgroundGradientClass = "backgroundGradienteRojo";
    else if (color === COLOR.AZUL)
      backgroundGradientClass = "backgroundGradienteAzul";
    else if (color === COLOR.VERDE)
      backgroundGradientClass = "backgroundGradienteVerde";

    return backgroundGradientClass;
}