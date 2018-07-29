using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ValidacionInmediatoSuperior
    {
        public ValidacionInmediatoSuperior()
        {
            Exepciones = new HashSet<Exepciones>();
        }

        public int IdValidacionJefe { get; set; }
        public int IdFormularioAnalisisOcupacional { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual ICollection<Exepciones> Exepciones { get; set; }
        public virtual FormularioAnalisisOcupacional IdFormularioAnalisisOcupacionalNavigation { get; set; }
    }
}
