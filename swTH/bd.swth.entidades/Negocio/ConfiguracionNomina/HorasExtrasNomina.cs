using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.Negocio
{
   public class HorasExtrasNomina
    {
        [Key]
        public int IdHorasExtrasNomina { get; set; }
        public int CantidadHoras { get; set; }
        public string IdentificacionEmpleado { get; set; }
        public bool EsExtraordinaria { get; set; }

        /// <summary>
        /// propiedades virtuales relaciones con otras tablas
        /// </summary>
        public int IdCalculoNomina { get; set; }
        public virtual CalculoNomina CalculoNomina { get; set; }
    }
}
