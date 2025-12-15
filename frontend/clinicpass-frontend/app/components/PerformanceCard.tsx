"use client";

import { useEffect, useState } from "react";
import { TrendingUp } from "lucide-react";

type ApiStatus = "loading" | "ok" | "error";

export default function PerformanceCard() {
  const [status, setStatus] = useState<ApiStatus>("loading");

  useEffect(() => {
    const checkApi = async () => {
      try {
        const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}`, {
          cache: "no-store",
        });

        if (!res.ok) throw new Error();
        setStatus("ok");
      } catch {
        setStatus("error");
      }
    };

    checkApi();
  }, []);

  return (
    <div className="bg-gradient-to-br from-indigo-500 to-purple-600 rounded-xl shadow-md p-6 text-white">
      <TrendingUp className="w-8 h-8 mb-4" />
      <h3 className="mb-2">Rendimiento</h3>

      <p className="mb-4 opacity-90">
        {status === "loading" && "Verificando estado del sistema…"}
        {status === "ok" && "El sistema está funcionando correctamente"}
        {status === "error" && "Problemas al conectar con la API"}
      </p>

      <div className="flex items-center gap-2">
        <div
          className={`w-2 h-2 rounded-full ${
            status === "loading"
              ? "bg-yellow-400 animate-pulse"
              : status === "ok"
              ? "bg-green-400 animate-pulse"
              : "bg-red-400"
          }`}
        />
        <span>
          {status === "loading" && "Comprobando…"}
          {status === "ok" && "Sistema operativo"}
          {status === "error" && "Sistema caído"}
        </span>
      </div>
    </div>
  );
}
