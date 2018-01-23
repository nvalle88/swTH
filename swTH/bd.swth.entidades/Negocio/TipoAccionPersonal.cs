namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TipoAccionPersonal
    {

        public int IdTipoAccionPersonal { get; set; }

        public string Nombre { get; set; }

        public int NDiasMinimo { get; set; }

        public int NDiasMaximo { get; set; }

        public int NHorasMinimo { get; set; }

        public int NHorasMaximo { get; set; }

        public bool DiasHabiles { get; set; }

        public bool ImputableVacaciones { get; set; }

        public bool ProcesoNomina { get; set; }

        public bool EsResponsableTH { get; set; }

        public string Matriz { get; set; }

        public string Descripcion { get; set; }

        public bool GeneraAccionPersonal { get; set; }

        public bool ModificaDistributivo { get; set; }

        public int IdEstadoTipoAccionPersonal { get; set; }
        public virtual EstadoTipoAccionPersonal EstadoTipoAccionPersonal { get; set; }
        //Propiedades Virtuales Referencias a otras clases

        public virtual ICollection<AccionPersonal> AccionPersonal { get; set; }
    }
}
