import { createStore, combineReducers } from 'redux';
import seccionPrincipalReducer from './seccion-principal/reducer';
import torneoReducer from './torneo/reducer';
import zonaReducer from './zona/reducer';
import faseReducer from './fase/reducer';
import opcionReducer from './opcion/reducer';

const reducers = combineReducers({
    torneoReducer,
    seccionPrincipalReducer,
    zonaReducer,
    faseReducer,
    opcionReducer
});

const store = createStore(
    reducers,
    window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

export default store;