using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class ImpuestoRentaParametros
    {
        public int IdImpuestoRentaParametros { get; set; }
        public decimal FraccionBasica { get; set; }
        public decimal ExcesoHasta { get; set; }
        public int? ImpuestoFraccionBasica { get; set; }
        public int PorcentajeImpuestoFraccionExcedente { get; set; }
    }
}
