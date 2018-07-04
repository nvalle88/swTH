using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace bd.swth.entidades.Negocio
{
    public class PieFirma
    {
        [Key]
        public int IdPieFirma { get; set; }

        public int IdTipoAccionPersonal { get; set; }
        public virtual TipoAccionPersonal TipoAccionPersonal { get; set; }

        public int IdIndiceOcupacional { get; set; }
        public virtual IndiceOcupacional IndiceOcupacional { get; set; }

        public int Nivel { get; set; }

    }
}
