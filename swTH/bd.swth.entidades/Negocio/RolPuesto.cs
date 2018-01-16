namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
    public partial class RolPuesto
    {
        [Key]
        public int IdRolPuesto { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<AdministracionTalentoHumano> AdministracionTalentoHumano { get; set; }
        public virtual ICollection<ConfiguracionViatico> ConfiguracionViatico { get; set; }
        public virtual ICollection<IndiceOcupacional> IndiceOcupacional { get; set; }
    }
}
