namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class FondoFinanciamiento
    {
        [Key]
        public int IdFondoFinanciamiento { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<DistributivoHistorico> DistributivoHistorico { get; set; }
        public virtual ICollection<DistributivoSituacionActual> DistributivoSituacionActual { get; set; }
        public virtual ICollection<SolicitudViatico> SolicitudViatico { get; set; }

    }
}
