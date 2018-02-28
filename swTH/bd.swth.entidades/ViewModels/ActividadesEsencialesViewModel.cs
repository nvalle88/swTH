using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
   public class ActividadesEsencialesViewModel
    {
        public int IdActividadesEsenciales { get; set; }

        public int IdIndiceOcupacional { get; set; }

        public string Descripcion { get; set; }
    }
}
