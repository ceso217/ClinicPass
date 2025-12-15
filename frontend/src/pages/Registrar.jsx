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

        // Luego conectarás tu API:
        // await fetch("/api/profesionales", {
        //     method: "POST",
        //     headers: { "Content-Type": "application/json" },
        //     body: JSON.stringify(form)
        // });
    };

    return (
        <div className="max-w-md mx-auto bg-white p-6 rounded-xl shadow-lg mt-10">
            <h2 className="text-2xl font-bold mb-5 text-center text-blue-600">Registrar Profesional</h2>

            <form onSubmit={handleSubmit} className="space-y-4">

                <div>
                    <label className="block font-medium mb-1">Nombre completo</label>
                    <input
                        type="text"
                        name="nombre"
                        value={form.nombre}
                        onChange={handleChange}
                        required
                        className="w-full border border-gray-300 p-2 rounded-lg"
                    />
                </div>

                <div>
                    <label className="block font-medium mb-1">DNI</label>
                    <input
                        type="text"
                        name="dni"
                        value={form.dni}
                        onChange={handleChange}
                        className="w-full border border-gray-300 p-2 rounded-lg"
                    />
                </div>

                <div>
                    <label className="block font-medium mb-1">Teléfono</label>
                    <input
                        type="text"
                        name="telefono"
                        value={form.telefono}
                        onChange={handleChange}
                        className="w-full border border-gray-300 p-2 rounded-lg"
                    />
                </div>

                <div>
                    <label className="block font-medium mb-1">Especialidad</label>
                    <input
                        type="text"
                        name="especialidad"
                        value={form.especialidad}
                        onChange={handleChange}
                        className="w-full border border-gray-300 p-2 rounded-lg"
                    />
                </div>

                <div>
                    <label className="block font-medium mb-1">Email</label>
                    <input
                        type="email"
                        name="email"
                        value={form.email}
                        onChange={handleChange}
                        required
                        className="w-full border border-gray-300 p-2 rounded-lg"
                    />
                </div>

                <div>
                    <label className="block font-medium mb-1">Contraseña</label>
                    <input
                        type="password"
                        name="password"
                        value={form.password}
                        onChange={handleChange}
                        required
                        className="w-full border border-gray-300 p-2 rounded-lg"
                    />
                </div>

                <button
                    type="submit"
                    className="w-full bg-blue-600 font-semibold hover:bg-blue-700 text-white py-2 rounded-lg mt-4"
                >
                    Registrar
                </button>
            </form>
        </div>
    );
};

export default Registrar;

