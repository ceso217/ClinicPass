import React, { useState } from "react";

const Login = () => {
    const [form, setForm] = useState({
        email: "",
        password: "",
    });

    const [error, setError] = useState("");

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError("");

        console.log("Intentando login con:", form);

        // Cuando tengas tu API lista:
        /*
        try {
            const response = await fetch("https://localhost:5001/api/auth/login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(form),
            });

            if (!response.ok) {
                setError("Credenciales incorrectas");
                return;
            }

            const data = await response.json();
            console.log("Token recibido:", data.token);

            localStorage.setItem("token", data.token);

            window.location.href = "/dashboard";
        } catch (error) {
            setError("Error al conectar con el servidor");
        }
        */
    };

    return (
        <div style={{ maxWidth: "350px", margin: "0 auto" }}>
            <h2>Iniciar Sesión</h2>

            <form onSubmit={handleSubmit}>
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

                {error && <p style={{ color: "red" }}>{error}</p>}

                <button type="submit">Ingresar</button>
            </form>
        </div>
    );
};

export default Login;
