using System;
using System.Collections.Generic;
using System.Text;

namespace bd.swth.entidades.Utils
{
    public static class Mensaje
    {
        public static string Excepcion { get { return "Ha ocurrido una Excepción"; } }
        public static string ExisteRegistro { get { return "Existe un registro de igual información"; } }
        public static string Satisfactorio { get { return "La acción se ha realizado satisfactoriamente"; } }
        public static string Error { get { return "Ha ocurrido error inesperado"; } }
        public static string RegistroNoEncontrado { get { return "El registro solicitado no se ha encontrado"; } }
        public static string ModeloInvalido { get { return "El Módelo es inválido"; } }
        public static string BorradoNoSatisfactorio { get { return "No es posible eliminar el registro, existen relaciones que dependen de él"; } }
        public static string BorradoSatisfactorio { get { return "El registro se ha eliminado correctamente"; } }
        public static string GuardadoSatisfactorio { get { return "Los datos se han guardado correctamente"; } }
        public static string CorreoSatisfactorio { get { return "Correo enviado satisfactoriamente"; } }
        public static string ErrorCorreo { get { return "No pudo enviarse el correo"; } }
        public static string DependenciaSinJefe { get { return "no existe una persona a cargo de la dependencia"; } }
        public static string AccesoNoAutorizado { get { return "No tiene los permisos necesarios para acceder a este sitio"; } }
        public static string ProcesoDesactivado { get { return "Este proceso está desactivado"; } }
        public static string ProcesoFinalizadoSatisfactorio { get { return "Proceso finalizado correctamente"; } }
        public static string ErrorEditarDatos { get { return "Los datos ingresados ya existen y no pueden ser editados"; } }
        public static string ErrorActualizarArchivo { get { return "No se ha podido actualizar el archivo"; } }

        public static string SeleccioneIndice { get { return "Debe seleccionar un índice en: situación propuesta."; } }
        public static string ErrorValorEstadoMovimientoInterno { get { return "Valor de estado sin nombre"; } }
        public static string ExisteIdentificacion { get { return "La identificación ingresada corresponde a otra persona"; } }

        ////
        public static string ConceptoNoExiste { get { return "El concepto no existe en el proceso al cual pertenece el presente cálculo de nómina."; } }
        public static string EmpleadoNoExiste { get { return "Identificación del empleado no existe."; } }

        public static string ExisteVacacionRelacionLaboral { get { return "Solo se permite 1 registro por régimen laboral."; } }

        public static string AccionPersonalBloqueada { get { return "Esta acción ya se ha aprobado por todos los responsables de aprobación, no puede editarse"; } }

        public static string ErrorPorComparacionFechasTipoAccionPersonal { get { return "La hora mínimo debe ser menor a la hora máximo y el día mínimo debe ser menor al día máximo"; } }

        public static string ExistePartidaIndividual { get { return "El número de partida / código ingresado ya existe"; } }

        public static string NumeroPartidaObligatorio { get { return "El número de partida / código no puede ir vacío"; } }
        
        

    }
}