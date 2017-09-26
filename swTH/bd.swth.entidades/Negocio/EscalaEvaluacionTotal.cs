namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public partial class EscalaEvaluacionTotal
    {
        [Key]
        public int IdEscalaEvaluacionTotal { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [Display(Name = "Nombre:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [Display(Name = "Descripción:")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [Range(0.1, 100, ErrorMessage = "El {0} no puede ser más de {2} ni menos de {1}")]
        [Display(Name = "Porciento Desde:")]
        [DisplayFormat(DataFormatString = "{0:0.00}%", ApplyFormatInEditMode = false)]
        public decimal? PorcientoDesde { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [Range(0.1, 100, ErrorMessage = "El {0} no puede ser más de {2} ni menos de {1}")]
        [Display(Name = "Porciento Hasta:")]
        [DisplayFormat(DataFormatString = "{0:0.00}%", ApplyFormatInEditMode = false)]
        public decimal? PorcientoHasta { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
    }
}
