import { FiltroFecha } from "./filtroFecha";

export interface FiltroFechaDTO {
  fechaInicio?: Date;
  fechaFin?: Date;
  tipoFiltro: number;
}
