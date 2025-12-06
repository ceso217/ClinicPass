import React, { useState } from "react";

const Registrar = () => {
    const [form, setForm] = useState({
        nombre: "",
        dni: "",
        telefono: "",
        especialidad: "",
        email: "",
        password: "",
    });

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        console.log("Datos del profesional:", form);

        // Cuando tengas la API lista:
        // await fetch("https://localhost:5001/api/profesionales", {
        //     method: "POST",
        //     headers: { "Content-Type": "application/json" },
        //     body: JSON.stringify(form)
        // });
    };

    return (
        <div style={{ maxWidth: "400px", margin: "0 auto" }}>
            <h2>Registrar Profesional</h2>

            <form onSubmit={handleSubmit}>
                <div>
                    <label>Nombre completo</label>
                    <input
                        type="text"
                        name="nombre"
                        value={form.nombre}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div>
                    <label>Dni</label>
                    <input
                        type="text"
                        name="dni"
                        value={form.telefono}
                        onChange={handleChange}
                    />
                </div>


                <div>
                    <label>Teléfono</label>
                    <input
                        type="text"
                        name="telefono"
                        value={form.telefono}
                        onChange={handleChange}
                    />
                </div>

                <div>
                    <label>Especialidad</label>
                    <input
                        type="text"
                        name="especialidad"
                        value={form.especialidad}
                        onChange={handleChange}
                    />
                </div>

                <div>
                    <label>Email</label>
                    <input
                        type="email"
                        name="email"
                        value={form.email}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label>Contraseña</label>
                    <input
                        type="password"
                        name="password"
                        value={form.password}
                        onChange={handleChange}
                        required
                    />
                </div>
                <button type="submit">Registrar</button>
            </form>
        </div>
    );
};

export default Registrar;
