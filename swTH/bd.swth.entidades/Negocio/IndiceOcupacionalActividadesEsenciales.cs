namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class IndiceOcupacionalActividadesEsenciales
    {
        [Key]
        public int IdIndiceOcupacionalActividadesEsenciales { get; set; }
        public int IdActividadesEsenciales { get; set; }
        public int IdIndiceOcupacional { get; set; }

        public virtual ActividadesEsenciales ActividadesEsenciales { get; set; }
        public virtual IndiceOcupacional IndiceOcupacional { get; set; }

    }
}
