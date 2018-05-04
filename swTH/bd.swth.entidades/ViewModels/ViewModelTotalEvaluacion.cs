using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class ViewModelTotalEvaluacion
    {
        public int IdEval001 { get; set; }
        public double TotalActividades { get; set; }
        public double TotalConocimiento { get; set; }
        public double TotalCompetenciasTecnicas { get; set; }
        public double TotalCompetenciasUniversales { get; set; }
        public double TotalTrabajoLiderazgo { get; set; }
        public double TotalQuejas { get; set; }
    }
}
