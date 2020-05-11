const noticiaReducer = (state = "", action) => {    
    if (action.type === 'ACTUALIZAR_NOTICIA') {
        return {
             ...state,
            noticia: action.payload
        }
    }
    return state;
};

export default noticiaReducer;