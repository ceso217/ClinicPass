/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: ['class', '[data-mode="dark"]'],
  content: [
    './app/**/*.{js,ts,jsx,tsx}', // Escanea todos los archivos dentro de la carpeta 'app'
    './src/**/*.{js,ts,jsx,tsx}', // Si usas una carpeta 'src'
    // ... otros paths si existen
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
