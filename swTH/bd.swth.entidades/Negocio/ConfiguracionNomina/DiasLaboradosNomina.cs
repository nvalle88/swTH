using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.Negocio
{
    public class DiasLaboradosNomina
    {
        [Key]
        public int IdDiasLaboradosNomina { get; set; }
        public string IdentificacionEmpleado { get; set; }
        public string NombreApellidoEmpleado  { get; set; }
        public int CantidadDias { get; set; }

        public int IdCalculoNomina { get; set; }
        public virtual CalculoNomina CalculoNomina { get; set; }
    }
}
