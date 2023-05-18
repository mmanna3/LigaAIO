/* eslint-disable quotes */
/* eslint-disable no-undef */

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
 
  theme: {
    extend: {
      screens: {
        'xs': '300px'
      },
      fontFamily: {
        'coalition': 'Coalition',
        'arial': ' Arial, Helvetica, sans-serif'
      },
      fontSize: {
        'xs': '12px',
      },
      backgroundImage: {
        'fondo-pc': "url('assets/images/desktop/fondo-pc.avif')",
        'fondo-celu': "url('assets/images/mobile/fondo-celu.avif')",
      },
      colors: {
        'title-darkGreen': '#579e74',
      },

    },
  },
  plugins: [],
};
