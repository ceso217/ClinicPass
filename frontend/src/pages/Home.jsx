import React from "react";
import "./Home.css"; // Opcional, si vas a usar estilos separados

const Home = () => {
    return (
        <div className="home-container">
            <header className="home-header">
                <h1>Bienvenido a ClinicPass</h1>
                <p className="home-subtitle">
                    Sistema de gestión para profesionales y pacientes.
                </p>
            </header>

            <section className="home-content">
                <div className="home-card">
                    <h2>Pacientes</h2>
                    <p>Gestioná el listado de pacientes registrados.</p>
                    <button className="home-button">Ver pacientes</button>
                </div>

                <div className="home-card">
                    <h2>Turnos</h2>
                    <p>Consultá y administrá turnos fácilmente.</p>
                    <button className="home-button">Ver turnos</button>
                </div>

                <div className="home-card">
                    <h2>Profesionales</h2>
                    <p>Accedé al listado y perfil de profesionales.</p>
                    <button className="home-button">Ver profesionales</button>
                </div>
            </section>
        </div>
    );
};

export default Home;
