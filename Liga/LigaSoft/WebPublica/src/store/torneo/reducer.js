const torneoReducer = (state = "", action) => {    
    if (action.type === 'ACTUALIZAR_TORNEO') {
        return {
             ...state,
            torneo: action.payload
        }
    }
    return state;
};

export default torneoReducer;