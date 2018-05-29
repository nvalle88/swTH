
namespace bd.swth.entidades.ViewModels
{
    using bd.swth.entidades.Negocio;
    using System.Collections.Generic;
    using System.ComponentModel;
    public class ViewModelEvaluacionCapacitaciones
    {
        public int IdEmpleado { get; set; }
        public int IdPlanCapacitacion { get; set; }
        public string NombreUsuario { get; set; }
        public string Identificacion { get; set; }
        public string NombreEvento { get; set; }
        public string Institucion { get; set; }
        public string LugarFecha { get; set; }
       //LIstas
        public List<PreguntaEvaluacionEvento> ListaPreguntaEvaluacionEvento { get; set; }
        public List<PlanCapacitacion> ListaPlanCapacitacion { get; set; }
        public List<string> PreguntaEvaluacionEvento { get; set; }
        

    }
}
