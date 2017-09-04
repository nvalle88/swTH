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

        [Range(0.1, 100, ErrorMessage = "El {0} no puede ser más de {2} ni menos de {1}")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:0.00}%", ApplyFormatInEditMode = false)]
        public decimal PorcientoDesde { get; set; }

        [Range(0.1, 100, ErrorMessage = "El {0} no puede ser más de {2} ni menos de {1}")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:0.00}%", ApplyFormatInEditMode = false)]
        public decimal PorcientoHasta { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
    }
}
