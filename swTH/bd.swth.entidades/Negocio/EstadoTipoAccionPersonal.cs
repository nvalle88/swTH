using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace bd.swth.entidades.Negocio
{
    public class EstadoTipoAccionPersonal
    {
        [Key]
        public int IdEstadoTipoAccionPersonal { get; set; }

        public string  Nombre { get; set; }
      
    }
}
