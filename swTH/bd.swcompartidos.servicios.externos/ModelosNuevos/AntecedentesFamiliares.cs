using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class AntecedentesFamiliares
    {
        public int IdAntecedentesFamiliares { get; set; }
        public string Parentesco { get; set; }
        public string Enfermedad { get; set; }
        public string Observaciones { get; set; }
        public int IdFichaMedica { get; set; }

        public virtual FichaMedica IdFichaMedicaNavigation { get; set; }
    }
}
