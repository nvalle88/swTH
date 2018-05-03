﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.Negocio
{
   public class ConstanteNomina
    {
        [Key]
        public int IdConstante { get; set; }
        public string Constante { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
    }
}