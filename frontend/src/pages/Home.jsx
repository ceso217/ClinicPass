import { Link } from "react-router-dom";

export default function Home() {
    return (
        <div className="min-h-screen bg-gray-100 flex flex-col items-center justify-center px-4">

            {/* Título */}
            <h1 className="text-4xl md:text-5xl font-bold text-blue-600 mb-10 text-center">
                Bienvenidos a ClinicPass
            </h1>

            {/* Card de Login */}
            <div className="bg-white shadow-lg rounded-xl p-8 w-full max-w-md">

                <h2 className="text-2xl font-semibold text-gray-800 mb-6 text-center">
                    Iniciar Sesión
                </h2>

                {/* Email */}
                <div className="mb-4">
                    <label className="block text-gray-700 font-medium mb-1">Email</label>
                    <input
                        type="email"
                        className="w-full border rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        placeholder="profesional@clinicpass.com"
                    />
                </div>

                {/* Contraseña */}
                <div className="mb-6">
                    <label className="block text-gray-700 font-medium mb-1">Contraseña</label>
                    <input
                        type="password"
                        className="w-full border rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        placeholder="••••••••"
                    />
                </div>

                {/* Botón Iniciar */}
                <button className="w-full bg-blue-600 text-white py-2 rounded-lg font-semibold hover:bg-blue-700 transition">
                    Iniciar Sesión
                </button>

                {/* Registrarse */}
                <p className="text-center mt-4 text-gray-600">
                    ¿No tenés cuenta?{" "}
                    <Link
                        to="/registrar"
                        className="text-blue-600 font-medium hover:underline"
                    >
                        Registrarse
                    </Link>
                </p>
            </div>
        </div>
    );
}
