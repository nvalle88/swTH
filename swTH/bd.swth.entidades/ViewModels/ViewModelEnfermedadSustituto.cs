using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class EnfermedadSustitutoRequest
    {
        public int IdPersonaSustituto { get; set; }
        public int IdEnfermedadSustituto { get; set; }

        public int IdTipoEnfermedad { get; set; }

        public string NombreTipoEnfermedad { get; set; }

        public string InstitucionEmite { get; set; }

        public bool PresentaCertificado { get; set; }

        public List<ViewModelEnfermedadSustituto> ListaEnfermedadesSustitutos { get; set; }
    }

    public class ViewModelEnfermedadSustituto
    {
        public int IdEnfermedadSustituto { get; set; }

        public int IdTipoEnfermedad { get; set; }

        public string NombreTipoEnfermedad { get; set; }

        public string InstitucionEmite { get; set; }

        public int IdPersonaSustituto { get; set; }
    }
}
