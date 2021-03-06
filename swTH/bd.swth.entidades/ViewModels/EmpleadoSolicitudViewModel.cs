﻿using bd.swth.entidades.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.ViewModels
{
    public class EmpleadoSolicitudViewModel 
    {

        public string NombreApellido { get; set; }
        public string Identificacion { get; set; }
        [Display(Name = "Revisado")]
        public bool Aprobado { get; set; }
        public bool HaSolicitado { get; set; }
        public int IdEmpleado { get; set; }
        public int IdSolicitud { get; set; }
        public List<SolicitudViatico> ListaSolicitudViatico { get; set; }
    }
}
