namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PlanGestionCambio
    {
        [Key]
        public int IdPlanGestionCambio { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        public int RealizadoPor { get; set; }
        
        public virtual Empleado EmpleadoRealizadoPor { get; set; }

        public int AprobadoPor { get; set; }
        public virtual Empleado EmpleadoAprobadoPor { get; set; }

        public virtual ICollection<ActividadesGestionCambio> ActividadesGestionCambio { get; set; }

        
    }
}
