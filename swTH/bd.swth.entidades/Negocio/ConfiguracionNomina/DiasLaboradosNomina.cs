using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace bd.swth.entidades.Negocio
{
    public class DiasLaboradosNomina
    {
        [Key]
        public int IdDiasLaboradosNomina { get; set; }
        public string IdentificacionEmpleado { get; set; }
        public int CantidadDias { get; set; }

        [NotMapped]
        public bool Valido { get; set; }
        [NotMapped]
        public string MensajeError { get; set; }
        [NotMapped]
        public string Nombres { get; set; }
        [NotMapped]
        public string Apellidos { get; set; }

        public int IdEmpleado { get; set; }
        public Empleado Empleado { get; set; }

        public int IdCalculoNomina { get; set; }
        public virtual CalculoNomina CalculoNomina { get; set; }
    }
}
