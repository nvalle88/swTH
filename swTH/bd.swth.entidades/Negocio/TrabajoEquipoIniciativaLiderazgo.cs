namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
     
    public partial class TrabajoEquipoIniciativaLiderazgo
    {
        [Key]
        public int IdTrabajoEquipoIniciativaLiderazgo { get; set; }
        public string Nombre { get; set; }
    }
}
