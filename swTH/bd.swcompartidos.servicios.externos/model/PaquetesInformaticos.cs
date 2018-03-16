using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class PaquetesInformaticos
    {
        public PaquetesInformaticos()
        {
            PersonaPaquetesInformaticos = new HashSet<PersonaPaquetesInformaticos>();
        }

        public int IdPaquetesInformaticos { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<PersonaPaquetesInformaticos> PersonaPaquetesInformaticos { get; set; }
    }
}
