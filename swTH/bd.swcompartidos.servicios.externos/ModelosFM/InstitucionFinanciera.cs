using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class InstitucionFinanciera
    {
        public InstitucionFinanciera()
        {
            DatosBancarios = new HashSet<DatosBancarios>();
        }

        public int IdInstitucionFinanciera { get; set; }
        public string Nombre { get; set; }
        public int? Spi { get; set; }

        public virtual ICollection<DatosBancarios> DatosBancarios { get; set; }
    }
}
