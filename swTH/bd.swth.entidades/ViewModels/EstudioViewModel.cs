using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
   public class EstudioViewModel
    {
        public int IdEstudio { get; set; }

        public int IdIndiceOcupacional { get; set; }

        public string Nombre { get; set; }
    }
}
