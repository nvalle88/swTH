using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Libros
    {
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public decimal? Precio { get; set; }
    }
}
