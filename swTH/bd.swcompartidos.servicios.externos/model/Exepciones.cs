using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Exepciones
    {
        public int IdExepciones { get; set; }
        public string Detalle { get; set; }
        public int IdValidacionJefe { get; set; }

        public virtual ValidacionInmediatoSuperior IdValidacionJefeNavigation { get; set; }
    }
}
