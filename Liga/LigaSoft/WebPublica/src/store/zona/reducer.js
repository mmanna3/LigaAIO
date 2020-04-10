const zonaReducer = (state = "", action) => {
    if (action.type === 'ACTUALIZAR_ZONA') {
        return {
             ...state,
            zona: action.payload
        }
    }
    return state;
};

export default zonaReducer;