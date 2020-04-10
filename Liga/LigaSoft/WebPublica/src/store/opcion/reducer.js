const opcionReducer = (state = "", action) => {
    if (action.type === 'ACTUALIZAR_OPCION') {
        return {
             ...state,
            opcion: action.payload
        }
    }
    return state;
};

export default opcionReducer;