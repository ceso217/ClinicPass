import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Registrar from "./pages/Registrar";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Layout from "./components/Layout";

function App() {
    return (
        <BrowserRouter>
            <Layout>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/registrar" element={<Registrar />} />
                    <Route path="/dashboard" element={<Dashboard />} />
                    <Route path="/login" element={<Login />} />
                </Routes>
            </Layout>
        </BrowserRouter>
    );
}

export default App;

