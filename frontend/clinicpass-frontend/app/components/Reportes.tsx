"use client";
import React, { useState, useMemo, useEffect } from "react";
import {
  Download,
  Filter,
  Calendar,
  TrendingUp,
  Users,
  FileText,
  Clock,
  BarChart3,
  PieChart,
  Activity
} from 'lucide-react';
import toast from 'react-hot-toast';
import jsPDF from 'jspdf';
import { useRef } from 'react';
import html2canvas from 'html2canvas';
import { toPng } from 'html-to-image';
  Activity,
} from "lucide-react";
import toast from "react-hot-toast";
import { FiltroFecha } from "../types/filtroFecha";
import { getTurnosPeriodo } from "../hooks/turnosApi";
import { getPacientesAtendidos } from "../hooks/pacientesApi";
import { getNumeroFichasFiltro } from "../hooks/fichasDeSeguimientoApi";
import {
  getProfesionalesActivos,
  getTotalProfesionales,
} from "../hooks/profesionalesApi";
import { FiltroFechaDTO } from "../types/filtroFechaDTO";

interface ReporteData {
  mes: string;
  turnos: number;
  pacientes: number;
  fichas: number;
}

interface ReportesStats {
  totalTurnos: number;
  pacientesAtendidos: number;
  fichasCompletadas: number;
  tasaOcupacion: number;
}

export const Reportes: React.FC = () => {
  const [tipoReporte, setTipoReporte] = useState<'general' | 'turnos' | 'pacientes' | 'profesionales'>('general');
  const pdfRef = useRef<HTMLDivElement>(null);
  const [tipoReporte, setTipoReporte] = useState<
    "general" | "turnos" | "pacientes" | "profesionales"
  >("general");
  const [periodo, setPeriodo] = useState<FiltroFecha>(FiltroFecha.UltimaSemana);
  const [fechaInicio, setFechaInicio] = useState();
  const [fechaFin, setFechaFin] = useState();
  const [stats, setStats] = useState<ReportesStats>({
    totalTurnos: 0,
    pacientesAtendidos: 0,
    fichasCompletadas: 0,
    tasaOcupacion: 0,
  });

  const etiquetasFiltro: Record<FiltroFecha, string> = {
    [FiltroFecha.Hoy]: "Hoy",
    [FiltroFecha.UltimaSemana]: "Última Semana",
    [FiltroFecha.UltimoMes]: "Último Mes",
    [FiltroFecha.UltimoTrimestre]: "Último Trimestre",
    [FiltroFecha.UltimoAno]: "Último Año",
    [FiltroFecha.Personalizado]: "Rango Personalizado",
  };
  // const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchStats({ tipoFiltro: FiltroFecha.UltimaSemana });
  }, []);

  const fetchStats = async (filtro: FiltroFechaDTO) => {
    try {
      // setLoading(true);

      const totalTurnos = await getTurnosPeriodo(filtro);
      const pacientesAtendidos = await getPacientesAtendidos(filtro);
      const fichasCompletadas = await getNumeroFichasFiltro(filtro);
      const profesionalesActivos = await getProfesionalesActivos();
      const profesionalesTotales = await getTotalProfesionales();
      const tasaOcupacion = profesionalesActivos / profesionalesTotales;

      console.log({
        totalTurnos,
        pacientesAtendidos,
        fichasCompletadas,
        tasaOcupacion,
      });

      setStats({
        totalTurnos: totalTurnos,
        pacientesAtendidos: pacientesAtendidos,
        fichasCompletadas: fichasCompletadas,
        tasaOcupacion: tasaOcupacion,
      });
    } catch (error) {
      console.error("Error al cargar estadísticas:", error);
      toast.error(`Error al cargar estadísticas : ${error}`);
      // setLoading(false);
    } finally {
      // setLoading(false);
    }
  };

  const handlePeriodoChange = async (
    e: React.ChangeEvent<HTMLSelectElement>
  ) => {
    const nuevoPeriodo = e.target.value as FiltroFecha;
    setPeriodo(nuevoPeriodo);

    if (nuevoPeriodo !== FiltroFecha.Personalizado) {
      await fetchStats({
        tipoFiltro: nuevoPeriodo,
      });
    }
  };

  const handleAplicarFechasPersonalizadas = async () => {
    if (fechaInicio && fechaFin) {
      console.log(fechaFin, fechaInicio);
      await fetchStats({
        tipoFiltro: FiltroFecha.Personalizado,
        fechaInicio,
        fechaFin,
      });
    } else {
      toast.error("Seleccione ambas fechas para filtrar.");
    }
  };

  // Datos mock para gráficos
  const datosGenerales = useMemo<ReporteData[]>(
    () => [
      { mes: "Ene", turnos: 145, pacientes: 89, fichas: 132 },
      { mes: "Feb", turnos: 168, pacientes: 102, fichas: 155 },
      { mes: "Mar", turnos: 192, pacientes: 115, fichas: 178 },
      { mes: "Abr", turnos: 178, pacientes: 98, fichas: 164 },
      { mes: "May", turnos: 201, pacientes: 123, fichas: 189 },
      { mes: "Jun", turnos: 187, pacientes: 110, fichas: 172 },
      { mes: "Jul", turnos: 156, pacientes: 95, fichas: 145 },
      { mes: "Ago", turnos: 210, pacientes: 128, fichas: 195 },
      { mes: "Sep", turnos: 198, pacientes: 118, fichas: 183 },
      { mes: "Oct", turnos: 225, pacientes: 135, fichas: 208 },
      { mes: "Nov", turnos: 234, pacientes: 142, fichas: 218 },
      { mes: "Dic", turnos: 189, pacientes: 112, fichas: 175 },
    ],
    []
  );

  const estadisticasPorEspecialidad = [
    { especialidad: "Cardiología", turnos: 456, porcentaje: 28 },
    { especialidad: "Pediatría", turnos: 389, porcentaje: 24 },
    { especialidad: "Traumatología", turnos: 312, porcentaje: 19 },
    { especialidad: "Dermatología", turnos: 278, porcentaje: 17 },
    { especialidad: "Clínica Médica", turnos: 195, porcentaje: 12 },
  ];

  const estadisticasPorEstado = [
    {
      estado: "Completados",
      cantidad: 1456,
      porcentaje: 72,
      color: "bg-green-500",
    },
    {
      estado: "Confirmados",
      cantidad: 345,
      porcentaje: 17,
      color: "bg-blue-500",
    },
    {
      estado: "Programados",
      cantidad: 178,
      porcentaje: 9,
      color: "bg-yellow-500",
    },
    { estado: "Cancelados", cantidad: 41, porcentaje: 2, color: "bg-red-500" },
  ];

  const profesionalesMasActivos = [
    {
      nombre: "Dra. María González",
      turnos: 487,
      fichas: 456,
      especialidad: "Cardiología",
    },
    {
      nombre: "Dr. Juan Pérez",
      turnos: 398,
      fichas: 378,
      especialidad: "Pediatría",
    },
    {
      nombre: "Dra. Laura Sánchez",
      turnos: 356,
      fichas: 334,
      especialidad: "Traumatología",
    },
    {
      nombre: "Dra. Carmen Díaz",
      turnos: 289,
      fichas: 267,
      especialidad: "Dermatología",
    },
    {
      nombre: "Dr. Roberto García",
      turnos: 234,
      fichas: 198,
      especialidad: "Clínica Médica",
    },
  ];

  const maxTurnos = Math.max(...datosGenerales.map((d) => d.turnos));
  const totalTurnos = datosGenerales.reduce((sum, d) => sum + d.turnos, 0);
  const totalPacientes = datosGenerales.reduce(
    (sum, d) => sum + d.pacientes,
    0
  );
  const totalFichas = datosGenerales.reduce((sum, d) => sum + d.fichas, 0);
  const promedioTurnos = Math.round(totalTurnos / datosGenerales.length);

  const handleExportarPDF = async () => {
   
  if (!pdfRef.current) return;

  toast.loading('Generando PDF...');

  try {
    const pdf = new jsPDF('p', 'mm', 'a4');
    const pdfWidth = pdf.internal.pageSize.getWidth();
    const pdfHeight = pdf.internal.pageSize.getHeight();

    const sections = pdfRef.current.querySelectorAll('.pdf-section');

    for (let i = 0; i < sections.length; i++) {
      const section = sections[i] as HTMLElement;

      const dataUrl = await toPng(section, {
        pixelRatio: 3,
        backgroundColor: '#f9fafb',
      });

      const img = new Image();
      img.src = dataUrl;
      await new Promise((res) => (img.onload = res));

      const imgHeight = (img.height * pdfWidth) / img.width;

      if (i !== 0) pdf.addPage();

      pdf.addImage(dataUrl, 'PNG', 0, 0, pdfWidth, imgHeight);
    }

    pdf.save('reporte-clinica.pdf');
    toast.dismiss();
    toast.success('PDF generado correctamente');
  } catch (err) {
    toast.dismiss();
    toast.error('Error al generar el PDF');
    console.error(err);
  }
};
  

  const handleExportarExcel = () => {
    toast.loading('Exportando reporte a Excel...');
  };

  const StatCard: React.FC<{
    icon: React.ReactNode;
    title: string;
    value: string | number;
    subtitle: string;
    color: string;
  }> = ({ icon, title, value, subtitle, color }) => (
    <div className="bg-white rounded-xl shadow-md p-6">
      <div className="flex items-start justify-between mb-4">
        <div className={`${color} p-3 rounded-lg`}>{icon}</div>
        <TrendingUp className="w-5 h-5 text-green-600" />
      </div>
      <p className="text-gray-600 mb-1">{title}</p>
      <p className="text-gray-900 mb-1">{value}</p>
      <p className="text-gray-500">{subtitle}</p>
    </div>
  );

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Header */}
      <div className="bg-white border-b border-gray-200 px-8 py-6">
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-gray-900">Reportes y Estadísticas</h1>
            <p className="text-gray-600 mt-1">
              Análisis detallado del rendimiento de la clínica
            </p>
          </div>
          <div className="flex gap-3">
            {/* <button
              onClick={handleExportarExcel}
              className="px-4 py-2 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50 transition flex items-center gap-2"
            >
              <Download className="w-5 h-5" />
              Excel
            </button> */}
            <button
              onClick={handleExportarPDF}
              className="bg-green-600 text-white px-4 py-2 rounded-lg hover:bg-green-700 transition flex items-center gap-2"
            >
              <Download className="w-5 h-5" />
              PDF
            </button>
          </div>
        </div>
      </div>

          <div className="p-8">
              <div ref={pdfRef} className="space-y-8">
                  <section className="pdf-section">
        {/* Filtros */}
        <div className="bg-white rounded-xl shadow-md p-6 mb-8">
          <div className="flex items-center gap-2 mb-4">
            <Filter className="w-5 h-5 text-gray-600" />
            <h2 className="text-gray-900">Filtros de Reporte</h2>
          </div>
          <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
            <div>
              <label className="block text-gray-700 mb-2">
                Tipo de Reporte
              </label>
              <select
                value={tipoReporte}
                onChange={(e) => setTipoReporte(e.target.value as any)}
                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
              >
                <option value="general">General</option>
                <option value="turnos">Turnos</option>
                <option value="pacientes">Pacientes</option>
                <option value="profesionales">Profesionales</option>
              </select>
            </div>
            <div>
              <label className="block text-gray-700 mb-2">Período</label>
              {/* <select
                value={periodo}
                onChange={handlePeriodoChange}
                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
              >
                {Object.values(FiltroFecha).map((valor) => (
                  <option key={valor} value={valor}>
                    {valor.replace(/([A-Z])/g, " $1").trim()}
                  </option>
                ))}
              </select> */}
              <select
                value={periodo}
                onChange={(e) => {
                  const valor = Number(e.target.value) as FiltroFecha;
                  setPeriodo(valor);
                  // Solo disparamos si no es personalizado
                  if (valor !== FiltroFecha.Personalizado) {
                    fetchStats({ tipoFiltro: valor });
                  }
                }}
                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
              >
                {Object.values(FiltroFecha)
                  // Filtramos para quedarnos solo con la parte numérica del Enum
                  .filter((v) => !isNaN(Number(v)))
                  .map((valor) => (
                    <option key={valor} value={valor}>
                      {etiquetasFiltro[valor as FiltroFecha]}
                    </option>
                  ))}
              </select>
            </div>
            {periodo === FiltroFecha.Personalizado && (
              <div className="w-full flex justify-around col-span-2">
                <div className="w-full mx-2 ">
                  <label className="block text-gray-700 mb-2">
                    Fecha Inicio
                  </label>
                  <input
                    type="date"
                    onChange={(e) => setFechaInicio(e.target.value)}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                  />
                </div>
                <div className="w-full mx-2 ">
                  <label className="block text-gray-700 mb-2">Fecha Fin</label>
                  <input
                    type="date"
                    onChange={(e) => setFechaFin(e.target.value)}
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
                  />
                </div>
                <div className="mx-2 my-1 h-1/2 self-end">
                  <button
                    className="bg-indigo-600  text-white px-4 py-2 rounded-lg hover:bg-indigo-700 transition flex items-center gap-2"
                    onClick={handleAplicarFechasPersonalizadas}
                  >
                    Aplicar
                  </button>
                </div>
              </div>
            )}
            {/* <div>
              <label className="block text-gray-700 mb-2">Fecha Inicio</label>
              <input
                type="date"
                value={fechaInicio}
                onChange={(e) => setFechaInicio(e.target.value)}
                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
              />
            </div>
            <div>
              <label className="block text-gray-700 mb-2">Fecha Fin</label>
              <input
                type="date"
                value={fechaFin}
                onChange={(e) => setFechaFin(e.target.value)}
                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500"
              />
            </div> */}
          </div>
        </div>

        {/* Tarjetas de estadísticas */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
          <StatCard
            icon={<Calendar className="w-6 h-6 text-white" />}
            title="Total Turnos"
            value={stats.totalTurnos}
            subtitle={`Promedio: ${promedioTurnos}/mes`}
            color="bg-blue-500"
          />
          <StatCard
            icon={<Users className="w-6 h-6 text-white" />}
            title="Pacientes Atendidos"
            value={stats.pacientesAtendidos}
            subtitle="Pacientes únicos"
            color="bg-green-500"
          />
          <StatCard
            icon={<FileText className="w-6 h-6 text-white" />}
            title="Fichas Completadas"
            value={stats.fichasCompletadas}
            subtitle={`${Math.round(
              (totalFichas / totalTurnos) * 100
            )}% de completitud`}
            color="bg-purple-500"
          />
          <StatCard
            icon={<Activity className="w-6 h-6 text-white" />}
            title="Tasa de Ocupación General"
            value={stats.tasaOcupacion * 100 + "%"}
            subtitle="Capacidad utilizada"
            color="bg-orange-500"
          />
        </div>

          <div className="grid grid-cols-1 lg:grid-cols-2 gap-8 mb-8">
            {/* Gráfico de línea - Evolución */}
            <div className="bg-white rounded-xl shadow-md p-6">
              <div className="flex items-center justify-between mb-6">
                <div className="flex items-center gap-2">
                  <TrendingUp className="w-5 h-5 text-indigo-600" />
                  <h3 className="text-gray-900">Evolución de Turnos</h3>
                </div>
                <div className="flex gap-4">
                  <div className="flex items-center gap-2">
                    <div className="w-3 h-3 bg-indigo-500 rounded-full"></div>
                    <span className="text-gray-600">Turnos</span>
                  </div>
                  <div className="flex items-center gap-2">
                    <div className="w-3 h-3 bg-green-500 rounded-full"></div>
                    <span className="text-gray-600">Pacientes</span>
                  </div>
                </div>
              </div>

            {/* Gráfico de línea simulado */}
            <div className="relative h-64">
              <svg className="w-full h-full" viewBox="0 0 600 250">
                {/* Líneas de guía */}
                <line
                  x1="40"
                  y1="10"
                  x2="40"
                  y2="210"
                  stroke="#e5e7eb"
                  strokeWidth="1"
                />
                <line
                  x1="40"
                  y1="210"
                  x2="590"
                  y2="210"
                  stroke="#e5e7eb"
                  strokeWidth="1"
                />

                {/* Líneas horizontales de referencia */}
                {[0, 50, 100, 150, 200].map((y, i) => (
                  <g key={i}>
                    <line
                      x1="40"
                      y1={10 + y}
                      x2="590"
                      y2={10 + y}
                      stroke="#f3f4f6"
                      strokeWidth="1"
                    />
                    <text
                      x="10"
                      y={15 + y}
                      className="text-gray-500"
                      fontSize="10"
                    >
                      {250 - i * 50}
                    </text>
                  </g>
                ))}

                {/* Línea de turnos */}
                <polyline
                  fill="none"
                  stroke="#6366f1"
                  strokeWidth="3"
                  points={datosGenerales
                    .map(
                      (d, i) =>
                        `${80 + i * 43},${210 - (d.turnos / maxTurnos) * 180}`
                    )
                    .join(" ")}
                />
                {/* Puntos de turnos */}
                {datosGenerales.map((d, i) => (
                  <circle
                    key={i}
                    cx={80 + i * 43}
                    cy={210 - (d.turnos / maxTurnos) * 180}
                    r="4"
                    fill="#6366f1"
                  />
                ))}

                {/* Línea de pacientes */}
                <polyline
                  fill="none"
                  stroke="#10b981"
                  strokeWidth="3"
                  strokeDasharray="5,5"
                  points={datosGenerales
                    .map(
                      (d, i) =>
                        `${80 + i * 43},${
                          210 - (d.pacientes / maxTurnos) * 180
                        }`
                    )
                    .join(" ")}
                />

                {/* Etiquetas de meses */}
                {datosGenerales.map((d, i) => (
                  <text
                    key={i}
                    x={80 + i * 43}
                    y="230"
                    textAnchor="middle"
                    className="text-gray-600"
                    fontSize="10"
                  >
                    {d.mes}
                  </text>
                ))}
              </svg>
            </div>
          </div>

            {/* Gráfico circular - Distribución por especialidad */}
            <div className="bg-white rounded-xl shadow-md p-6">
              <div className="flex items-center gap-2 mb-6">
                <PieChart className="w-5 h-5 text-indigo-600" />
                <h3 className="text-gray-900">Distribución por Especialidad</h3>
              </div>

            <div className="flex items-center justify-center mb-6">
              {/* Gráfico circular SVG */}
              <svg className="w-48 h-48" viewBox="0 0 200 200">
                <circle cx="100" cy="100" r="80" fill="#e0e7ff" />
                <circle
                  cx="100"
                  cy="100"
                  r="80"
                  fill="none"
                  stroke="#6366f1"
                  strokeWidth="40"
                  strokeDasharray="175 327"
                  transform="rotate(-90 100 100)"
                />
                <circle
                  cx="100"
                  cy="100"
                  r="80"
                  fill="none"
                  stroke="#3b82f6"
                  strokeWidth="40"
                  strokeDasharray="125 377"
                  strokeDashoffset="-175"
                  transform="rotate(-90 100 100)"
                />
                <circle
                  cx="100"
                  cy="100"
                  r="80"
                  fill="none"
                  stroke="#10b981"
                  strokeWidth="40"
                  strokeDasharray="100 402"
                  strokeDashoffset="-300"
                  transform="rotate(-90 100 100)"
                />
                <circle
                  cx="100"
                  cy="100"
                  r="80"
                  fill="none"
                  stroke="#f59e0b"
                  strokeWidth="40"
                  strokeDasharray="85 417"
                  strokeDashoffset="-400"
                  transform="rotate(-90 100 100)"
                />
                <circle
                  cx="100"
                  cy="100"
                  r="80"
                  fill="none"
                  stroke="#ef4444"
                  strokeWidth="40"
                  strokeDasharray="17 485"
                  strokeDashoffset="-485"
                  transform="rotate(-90 100 100)"
                />

                <text
                  x="100"
                  y="95"
                  textAnchor="middle"
                  className="text-gray-900"
                  fontSize="24"
                  fontWeight="bold"
                >
                  {estadisticasPorEspecialidad.reduce(
                    (sum, e) => sum + e.turnos,
                    0
                  )}
                </text>
                <text
                  x="100"
                  y="115"
                  textAnchor="middle"
                  className="text-gray-600"
                  fontSize="12"
                >
                  Turnos totales
                </text>
              </svg>
            </div>

            <div className="space-y-3">
              {estadisticasPorEspecialidad.map((esp, index) => {
                const colors = [
                  "bg-indigo-600",
                  "bg-blue-500",
                  "bg-green-500",
                  "bg-yellow-500",
                  "bg-red-500",
                ];
                return (
                  <div
                    key={esp.especialidad}
                    className="flex items-center justify-between"
                  >
                    <div className="flex items-center gap-2">
                      <div
                        className={`w-3 h-3 ${colors[index]} rounded-full`}
                      ></div>
                      <span className="text-gray-700">{esp.especialidad}</span>
                    </div>
                    <span className="text-gray-900">
                      {esp.turnos} ({esp.porcentaje}%)
                    </span>
                  </div>
                );
              })}
            </div>
          </div>
        </div>

        {/* Estadísticas por estado */}
        <div className="bg-white rounded-xl shadow-md p-6 mb-8">
          <h3 className="text-gray-900 mb-6">Estado de Turnos</h3>
          <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
            {estadisticasPorEstado.map((estado) => (
              <div key={estado.estado} className="text-center">
                <div
                  className={`${estado.color} h-24 rounded-lg mb-3 flex items-center justify-center`}
                >
                  <p className="text-white">{estado.cantidad}</p>
                </div>
                <p className="text-gray-900 mb-1">{estado.estado}</p>
                <p className="text-gray-600">{estado.porcentaje}%</p>
              </div>
            ))}
          </div>
        </div>

        {/* Profesionales más activos */}
        <div className="bg-white rounded-xl shadow-md p-6">
          <div className="flex items-center gap-2 mb-6">
            <Users className="w-5 h-5 text-indigo-600" />
            <h3 className="text-gray-900">Profesionales Más Activos</h3>
          </div>

          <div className="overflow-x-auto">
            <table className="w-full">
              <thead className="bg-gray-50 border-b border-gray-200">
                <tr>
                  <th className="px-6 py-3 text-left text-gray-900">Ranking</th>
                  <th className="px-6 py-3 text-left text-gray-900">
                    Profesional
                  </th>
                  <th className="px-6 py-3 text-left text-gray-900">
                    Especialidad
                  </th>
                  <th className="px-6 py-3 text-center text-gray-900">
                    Turnos
                  </th>
                  <th className="px-6 py-3 text-center text-gray-900">
                    Fichas
                  </th>
                  <th className="px-6 py-3 text-center text-gray-900">
                    Completitud
                  </th>
                </tr>
              </thead>
              <tbody className="divide-y divide-gray-200">
                {profesionalesMasActivos.map((prof, index) => {
                  const completitud = Math.round(
                    (prof.fichas / prof.turnos) * 100
                  );
                  return (
                    <tr key={prof.nombre} className="hover:bg-gray-50">
                      <td className="px-6 py-4">
                        <div className="flex items-center justify-center w-8 h-8 bg-indigo-100 text-indigo-700 rounded-full">
                          {index + 1}
                        </div>
                      </td>
                      <td className="px-6 py-4 text-gray-900">{prof.nombre}</td>
                      <td className="px-6 py-4 text-gray-700">
                        {prof.especialidad}
                      </td>
                      <td className="px-6 py-4 text-center text-gray-900">
                        {prof.turnos}
                      </td>
                      <td className="px-6 py-4 text-center text-gray-900">
                        {prof.fichas}
                      </td>
                      <td className="px-6 py-4 text-center">
                        <span
                          className={`px-3 py-1 rounded-full ${
                            completitud >= 90
                              ? "bg-green-100 text-green-700"
                              : completitud >= 75
                              ? "bg-yellow-100 text-yellow-700"
                              : "bg-red-100 text-red-700"
                          }`}
                        >
                          {completitud}%
                        </span>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
};
