namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ConfiguracionViatico
    {
        [Key]
        public int IdConfiguracionViatico { get; set; }
        public int IdRolPuesto { get; set; }
        public decimal? ValorEntregadoPorDia { get; set; }
        public string PorCientoAjustificar { get; set; }

        public virtual RolPuesto RolPuesto { get; set; }

        public virtual ICollection<SolicitudViatico> SolicitudViatico { get; set; }

    }
}
