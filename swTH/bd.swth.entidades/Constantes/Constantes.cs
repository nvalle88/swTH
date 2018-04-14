using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.Constantes
{
    public static class Constantes
    {
        // Constantes de correo 
        public static string Smtp;
        public static string PrimaryPort;
        public static string SecureSocketOptions;
        public static string CorreoTTHH;
        public static string PasswordCorreo;

        // Constante de estado para Documento Activacion Personal
        public static string ActivacionPersonalValorActivado;
        public static string ActivacionPersonalValorDesactivado;



        public static string PartidaVacante { get; set; }
    }

    public static class ConstantesGrupoOcupacional
    {
        // Constantes GrupoOcupacional
        public static string GrupoOcupacionalNivelSuperior;
        public static string GrupoOcupacionalNivelOperativo;
    }
}
