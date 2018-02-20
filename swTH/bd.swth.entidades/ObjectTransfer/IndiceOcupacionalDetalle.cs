using bd.swth.entidades.Negocio;
using System.Collections.Generic;

namespace bd.swth.entidades.ObjectTransfer
{
    public class IndiceOcupacionalDetalle
    {
        public IndiceOcupacional IndiceOcupacional { get; set; }
        public List<RelacionesInternasExternas> ListaRelacionesInternasExternas { get; set; }
        public List<Estudio> ListaEstudios { get; set; }
        public List<AreaConocimiento> ListaAreaConocimientos { get; set; }
        public List<ExperienciaLaboralRequerida> ListaExperienciaLaboralRequeridas { get; set; }
        public List<Capacitacion> ListaCapacitaciones { get; set; }
        public List<ActividadesEsenciales> ListaActividadesEsenciales { get; set; }
        public List<ConocimientosAdicionales> ListaConocimientosAdicionales { get; set; }
        public List<ComportamientoObservable> ListaComportamientoObservables { get; set; }

    }
}
