namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class CandidatoConcurso
    {
        [Key]
        public int IdCandidatoConcurso { get; set; }
        public string CodigoSocioEmpleo { get; set; }
        public bool? Preseleccionado { get; set; }
        public bool? Ganador { get; set; }
        public int IdCandidato { get; set; }
        public int IdPartidasFase { get; set; }

        public virtual ICollection<Calificacion> Calificacion { get; set; }
        public virtual Candidato Candidato { get; set; }
        public virtual PartidasFase PartidasFase{ get; set; }

    }
}
