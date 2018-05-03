namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ActividadesEsenciales
    {
        [Key]
        public int IdActividadesEsenciales { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<EvaluacionActividadesPuestoTrabajo> EvaluacionActividadesPuestoTrabajo { get; set; }
        public virtual ICollection<IndiceOcupacionalActividadesEsenciales> IndiceOcupacionalActividadesEsenciales { get; set; }
    }
}
