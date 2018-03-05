
using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class FichaMedicaViewModel
    {

        public DatosBasicosPersonaViewModel DatosBasicosPersonaViewModel { get; set; }
        public List<FichaMedica> FichasMedicas { get; set; }
        
    }
}
