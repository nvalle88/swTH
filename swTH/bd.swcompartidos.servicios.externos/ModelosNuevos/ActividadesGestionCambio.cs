using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ActividadesGestionCambio
    {
        public int IdActividadesGestionCambio { get; set; }
        public int IdDependencia { get; set; }
        public int IdEmpleado { get; set; }
        public int EstadoActividadesGestionCambio { get; set; }
        public string Tarea { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Avance { get; set; }
        public string Observaciones { get; set; }

        public virtual Dependencia IdDependenciaNavigation { get; set; }
        public virtual Empleado IdEmpleadoNavigation { get; set; }
    }
}
