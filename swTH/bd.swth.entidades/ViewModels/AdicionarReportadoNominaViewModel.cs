using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
   public class AdicionarReportadoNominaViewModel
    {
        public int IdEmpleado { get; set; }
        public int IdConcepto { get; set; }
        public double Valor { get; set; }
        public int IdCalculoNomina { get; set; }
    }
}
