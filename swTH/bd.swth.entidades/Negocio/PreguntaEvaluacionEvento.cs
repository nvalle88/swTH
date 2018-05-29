
namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    public partial class PreguntaEvaluacionEvento
    {
        

        public int IdPreguntaEvaluacionEvento { get; set; }
        public string Descripcion { get; set; }
        public bool? Facilitador { get; set; }
        public bool? Organizador { get; set; }
        public bool? ConocimientoObtenidos { get; set; }

        public virtual ICollection<DetalleEvaluacionEvento> DetalleEvaluacionEvento { get; set; }
    }
}
