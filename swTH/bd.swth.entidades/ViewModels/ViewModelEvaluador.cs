using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class ViewModelEvaluador
    {
        public int IdEmpleado { get; set; }
        public int IdEval001 { get; set; }
        public int IdJefe { get; set; }
        public int IdIndiceOcupacional { get; set; }
        public string NombreApellido { get; set; }
        public string Puesto { get; set; }
        public string Titulo { get; set; }
        public string DatosJefe { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public string NombreUsuario { get; set; }
        public List<ActividadesEsenciales> ListaActividad { get; set; }
        public List<string> ListaActividades { get; set; }
        public List<string> ListaIndicadores { get; set; }
        public List<string> ListaMetaPeriodo { get; set; }
        public List<string> ListaActividadescumplidos { get; set; }
        public List<string> ConocimientosEsenciales { get; set; }
        public List<string> IdAreaConocimiento { get; set; }
        public List<string> PorcentajeCumplido { get; set; }
        public List<string> NivelCumplimiento { get; set; }
        public string PorcentajeAumento { get; set; }
        //Competencias Tecnicas Puesto
        public List<string> IdComportamientoObervable { get; set; }
        public List<string> IdNivelDesarrollos { get; set; }
        public List<string> IdFrecuenciaAplicaciones { get; set; }

        //totales
        public int TotalConocimiento { get; set; }

        //Observaciones
        public string Observaciones { get; set; }


        // totales
        public double ActividadesTotal { get; set; }
        public double TotalConocimientos { get; set; }
        public double TotalCompetenciasTecnicas { get; set; }
        public double TotalCompetenciasUniversales { get; set; }
        public double TotalTrabajoLiderazgo { get; set; }
        public double TotalQuejas { get; set; }
        public double TotalEvaluacion { get; set; }
    }
}
