namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EvaluacionActividadesPuestoTrabajo
    {
        [Key]
        public int IdEvaluacionActividadesPuestoTrabajo { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [Display(Name = "Nombre:")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El {0} no puede tener más de {1} y menos de {2}")]
        
        public int IdIndiceOcupacionalActividadesEsenciales { get; set; }
        public int? IdIndicador { get; set; }
        public string DescripcionActividad { get; set; }
        public int MetaPeriodo { get; set; }
        public int ActividadesCumplidas { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
        public virtual Indicador Indicador { get; set; }
        public virtual IndiceOcupacionalActividadesEsenciales IndiceOcupacionalActividadesEsenciales { get; set; }
    }
}
