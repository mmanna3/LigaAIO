const faseReducer = (state = "", action) => {
    if (action.type === 'ACTUALIZAR_FASE') {
        return {
             ...state,
            fase: action.payload
        }
    }
    return state;
};

export default faseReducer;