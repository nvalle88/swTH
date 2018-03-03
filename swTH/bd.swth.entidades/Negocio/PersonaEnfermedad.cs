namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class PersonaEnfermedad
    {
        [Key]
        public int IdPersonaEnfermedad { get; set; }

  
        public string InstitucionEmite { get; set; }


        public int? IdTipoEnfermedad { get; set; }
        public virtual TipoEnfermedad TipoEnfermedad { get; set; }

        public int? IdPersona { get; set; }
        public virtual Persona Persona { get; set; }

    }
}
