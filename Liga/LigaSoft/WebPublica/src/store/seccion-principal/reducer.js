const seccionPrincipalReducer = (state = "", action) => {
    if (action.type === 'ACTUALIZAR_SECCION_PRINCIPAL') {
        return {
             ...state,
            seccionPrincipal: action.payload
        }
    }
    return state;
};

export default seccionPrincipalReducer;