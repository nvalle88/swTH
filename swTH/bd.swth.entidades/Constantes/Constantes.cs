using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.Constantes
{
    public static class Constantes
    {
        
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

    public static class ConstantesCorreo
    {
        // Constantes de correo 
        public static string Smtp;
        public static string PrimaryPort;
        public static string SecureSocketOptions;
        public static string CorreoTTHH;
        public static string PasswordCorreo;
        public static string NameFrom;
        public static string Subject;
        public static string MensajeCorreoSuperior;
        public static string MensajeCorreoDependencia;
        public static string MensajeCorreoMedio;
        public static string MensajeCorreoEnlace;
        public static string MensajeCorreoInferior;
        
    }

    public static class ConstantesEstadoInduccion {
        public static string InduccionFinalizada;
        public static string InduccionNoFinalizada;
    }

}
