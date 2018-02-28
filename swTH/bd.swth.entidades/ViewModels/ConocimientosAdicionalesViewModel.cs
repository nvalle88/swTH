using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
   public class ConocimientosAdicionalesViewModel
    {
        public int IdConocimientosAdicionales { get; set; }

        public int IdIndiceOcupacional { get; set; }

        public string Descripcion { get; set; }
    }
}
