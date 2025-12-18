import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./styles/global.css";
import { AuthProvider } from '@/app/contexts/AuthContext';
import { ThemeProvider } from '@/app/contexts/ThemeProvider';
import { Profesionales } from "./components/Profesionales";
import { Toaster } from "react-hot-toast";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "ClinicPass",
  description: "Sistema de Gestión Clínica",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body
        className={`${geistSans.variable} ${geistMono.variable} antialiased`}
      >
        <AuthProvider>
          <Toaster
            position="top-center"
            toastOptions={{
              style: {
                background: "#1f2937",   // gris oscuro
                color: "#fff",
                borderRadius: "10px",
                padding: "16px",
                fontSize: "14px",
              },
              success: {
                style: {
                  background: "#16a34a", // verde
                },
              },
              error: {
                style: {
                  background: "#dc2626", // rojo
                },
              },
            }}
          />
          <ThemeProvider>
        {children}
        </ThemeProvider>
        </AuthProvider>
      </body>
    </html>
  );
}
