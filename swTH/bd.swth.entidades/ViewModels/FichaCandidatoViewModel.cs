using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
   public class FichaCandidatoViewModel
    {

        public int IdCandidato { get; set; }

        public int IdPersona { get; set; }

        public string Identificacion { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string TelefonoPrivado { get; set; }

        public string TelefonoCasa { get; set; }

        public string CorreoPrivado { get; set; }
    }
}
