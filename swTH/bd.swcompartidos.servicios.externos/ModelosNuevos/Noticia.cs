using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Noticia
    {
        public int IdNoticia { get; set; }
        public DateTime? Fecha { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
    }
}
