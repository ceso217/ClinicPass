import { Link } from "react-router-dom";
import "./Navbar.css";

export default function Navbar() {
    return (
        <nav className="navbar">
            <div className="logo">
                <Link to="/">Logotipo ClinicPass</Link>
            </div>
            <div className="nav-links">
            <Link to="/registrar">Registrarse</Link>
            <Link to="/login">Login</Link>
            </div>
        </nav>
    );
}
