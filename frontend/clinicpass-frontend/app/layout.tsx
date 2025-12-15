import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./styles/global.css";
import { AuthProvider } from '@/app/contexts/AuthContext';
import { ThemeProvider } from '@/app/contexts/ThemeProvider';
import { Profesionales } from "./components/Profesionales";

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
          <ThemeProvider>
        {children}
        </ThemeProvider>
        </AuthProvider>
      </body>
    </html>
  );
}
