using System;
using System.Collections.Generic;
using System.Text;
using bd.swth.entidades.ViewModels;

namespace bd.swth.entidades.Constantes
{
    public static class Constantes
    {
        
        // Constante de estado para Documento Activacion Personal
        public static string ActivacionPersonalValorActivado;
        public static string ActivacionPersonalValorDesactivado;

        // Constante de estado de actividad de gestión de cambio
        public static List<EstadoActividadGestionCambioViewModel> ListaEstadosGestionCambio;

        public static string PartidaVacante { get; set; }
        public static string PartidaOcupada { get; set; }
        public static string PartidaSuprimida { get; set; }

        public static string EscapeConstantes { get { return "#"; } }
        public static string EscapeFunciones { get { return "@"; } }
        public static string EscapeConjuntos { get { return "&"; } }

        public static string NombreDesconcentrados { get; set; }
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
        public static string SubjectCapacitaciones;
        public static string Subject;
        public static string MensajeCorreoSuperior;
        public static string MensajeCorreoDependencia;
        public static string MensajeCorreoMedio;
        public static string MensajeCorreoEnlace;
        public static string MensajeCorreoInferior;

        //correo planificacion 
        public static string CorreoCabecera;
        public static string CorreoEnlace;
        public static string CorreoPie;

    }

    //Valores viaticos
    public static class ConstantesViaticos
    {
        public static string Operativo;
        public static string Jefe;
    }



    public static class ConstantesEstadoInduccion {
        public static string InduccionFinalizada;
        public static string InduccionNoFinalizada;
    }
    //Estados Capacitacion
    public static class ConstantesCapacitacion
    {
        public static int EstadoTerminado;
        public static int EstadoEvaluado;
        public static int Desactivado;
        public static int EstadoIngresado;
    }


    public static class ConstantesEstadosAprobacionMovimientoInterno
    {
        public static List<AprobacionMovimientoInternoViewModel> ListaEstadosAprobacionMovimientoInterno { get; set; }

    }

    public static class ConstantesEstadosVacaciones
    {
        public static List<EstadoVacacionesViewModel> ListaEstadosVacaciones { get; set; }

    }

    public static class ConstantesTipoRelacion
    {
        public static string Contrato;
        public static string Nombramiento;
    }

}
