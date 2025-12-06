import Navbar from "./Navbar";
import Footer from "./Footer";

export default function Layout({ children }) {
    return (
        <div>
            <Navbar />

            <main style={{ padding: "20px" }}>
                {children}
            </main>

            <Footer />
        </div>
    );
}
