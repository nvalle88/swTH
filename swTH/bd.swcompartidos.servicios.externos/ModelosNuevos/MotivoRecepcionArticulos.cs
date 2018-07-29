using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class MotivoRecepcionArticulos
    {
        public MotivoRecepcionArticulos()
        {
            OrdenCompra = new HashSet<OrdenCompra>();
        }

        public int IdMotivoRecepcionArticulos { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<OrdenCompra> OrdenCompra { get; set; }
    }
}
