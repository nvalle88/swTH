using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ModalidadPartida
    {
        public ModalidadPartida()
        {
            EmpleadoMovimiento = new HashSet<EmpleadoMovimiento>();
            IndiceOcupacionalModalidadPartida = new HashSet<IndiceOcupacionalModalidadPartida>();
        }

        public int IdModalidadPartida { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EmpleadoMovimiento> EmpleadoMovimiento { get; set; }
        public virtual ICollection<IndiceOcupacionalModalidadPartida> IndiceOcupacionalModalidadPartida { get; set; }
    }
}
