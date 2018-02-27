using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class ConfiguracionFeriados
    {
        public int IdConfiguracionFeriado { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string Descripcion { get; set; }
    }
}
