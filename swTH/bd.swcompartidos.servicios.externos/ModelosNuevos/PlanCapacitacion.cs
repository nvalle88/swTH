using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class PlanCapacitacion
    {
        public PlanCapacitacion()
        {
            EvaluacionEvento = new HashSet<EvaluacionEvento>();
        }

        public int IdPlanCapacitacion { get; set; }
        public int? IdGestionPlanCapacitacion { get; set; }
        public string NumeroPartidaPresupuestaria { get; set; }
        public string Institucion { get; set; }
        public string Pais { get; set; }
        public string Provincia { get; set; }
        public string NombreCiudad { get; set; }
        public string NivelDesconcentracion { get; set; }
        public string UnidadAdministrativa { get; set; }
        public string Cedula { get; set; }
        public string ApellidoNombre { get; set; }
        public string Sexo { get; set; }
        public string GrupoOcupacional { get; set; }
        public string DenominacionPuesto { get; set; }
        public string RegimenLaboral { get; set; }
        public string ModalidadLaboral { get; set; }
        public string TemaCapacitacion { get; set; }
        public string ClasificacionTema { get; set; }
        public string ProductoFinal { get; set; }
        public string Modalidad { get; set; }
        public int? Duracion { get; set; }
        public decimal? PresupuestoIndividual { get; set; }
        public DateTime? FechaCapacitacionPlanificada { get; set; }
        public int? TipoCapacitacion { get; set; }
        public int? EstadoEvento { get; set; }
        public int? AmbitoCapacitacion { get; set; }
        public int? NombreEvento { get; set; }
        public int? TipoEvento { get; set; }
        public int? IdProveedorCapacitaciones { get; set; }
        public int? DuracionEvento { get; set; }
        public int? Anio { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? ValorReal { get; set; }
        public int? IdCiudad { get; set; }
        public int? TipoEvaluacion { get; set; }
        public string Ubicacion { get; set; }
        public string Observacion { get; set; }
        public int? Estado { get; set; }

        public virtual ICollection<EvaluacionEvento> EvaluacionEvento { get; set; }
        public virtual GeneralCapacitacion AmbitoCapacitacionNavigation { get; set; }
        public virtual GeneralCapacitacion EstadoEventoNavigation { get; set; }
        public virtual Ciudad IdCiudadNavigation { get; set; }
        public virtual GestionPlanCapacitacion IdGestionPlanCapacitacionNavigation { get; set; }
        public virtual CapacitacionProveedor IdProveedorCapacitacionesNavigation { get; set; }
        public virtual GeneralCapacitacion NombreEventoNavigation { get; set; }
        public virtual GeneralCapacitacion TipoCapacitacionNavigation { get; set; }
        public virtual GeneralCapacitacion TipoEvaluacionNavigation { get; set; }
        public virtual GeneralCapacitacion TipoEventoNavigation { get; set; }
    }
}
