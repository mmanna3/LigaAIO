import React from 'react';
import ReactDOM from 'react-dom';
import Home from './components/Home/Home';
import { Provider } from 'react-redux';
import store from './store';
import Navbar from './components/Navbar/Navbar';
import bootstrap from "GlobalStyle/bootstrap.min.css";

const Application = () => (

    <Provider store={store}>
        <Navbar/>
        <div id="contenido" className={bootstrap.container}>
            <Home/>
        </div>
    </Provider>
);


ReactDOM.render(<Application />, document.getElementById('contenedor-padre'));