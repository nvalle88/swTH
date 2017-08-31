namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public partial class EscalaEvaluacionTotal
    {
        [Key]
        public int IdEscalaEvaluacionTotal { get; set; }

        
        public string Nombre { get; set; }

        
        public string Descripcion { get; set; }

 
        public decimal PorcientoDesde { get; set; }

 
        public decimal PorcientoHasta { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
    }
}
