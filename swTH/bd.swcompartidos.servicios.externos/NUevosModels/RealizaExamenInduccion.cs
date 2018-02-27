using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class RealizaExamenInduccion
    {
        public RealizaExamenInduccion()
        {
            DetalleExamenInduccion = new HashSet<DetalleExamenInduccion>();
        }

        public int IdRealizaExamenInduccion { get; set; }
        public string Fecha { get; set; }
        public decimal? Calificacion { get; set; }
        public int IdEvaluacionInduccion { get; set; }
        public int IdEmpleado { get; set; }

        public virtual ICollection<DetalleExamenInduccion> DetalleExamenInduccion { get; set; }
        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual EvaluacionInducion IdEvaluacionInduccionNavigation { get; set; }
    }
}
