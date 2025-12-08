import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Registrar from "./pages/Registrar";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Layout from "./components/Layout";
import SchedulePage from "./pages/SchedulePage";
import Profesionales from "./pages/Profesionales";
import DashboardPacientes from "./pages/Pacientes";

function App() {
    return (
        <BrowserRouter>
            <Routes>
                {/* Rutas sin layout */}
                <Route path="/" element={<Home />} />
                <Route path="/registrar" element={<Registrar />} />
                <Route path="/login" element={<Login />} />

                {/* Rutas con sidebar + topbar */}
                <Route element={<Layout />}>
                    <Route path="/dashboard" element={<Dashboard />} />
                    <Route path="/pacientes" element={<DashboardPacientes />} />
                    <Route path="/schedulepage" element={<SchedulePage />} />
                    <Route path="/profesionales" element={<Profesionales />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

export default App;

