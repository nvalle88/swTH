using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public bool EsCienPorciento { get; set; }



        [NotMapped]
        public bool Valido { get; set; }
        [NotMapped]
        public string MensajeError { get; set; }
        [NotMapped]
        public string Nombres { get; set; }

        [NotMapped]
        public string Apellidos { get; set; }
        /// <summary>
        /// propiedades virtuales relaciones con otras tablas
        /// </summary>
        public int IdCalculoNomina { get; set; }
        public virtual CalculoNomina CalculoNomina { get; set; }

        public int IdEmpleado { get; set; }
        public virtual Empleado Empleado { get; set; }

    }
}
