namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class PersonaEstudio
    {
        [Key]
        public int IdPersonaEstudio { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [Display(Name = "Fecha de graduado:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaGraduado { get; set; }

        
        public string Observaciones { get; set; }

        public string NoSenescyt { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        [Display(Name = "Título:")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0} ")]
        public int IdTitulo { get; set; }
        public virtual Titulo Titulo { get; set; }

        [Display(Name = "Persona:")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0} ")]
        public int IdPersona { get; set; }
        public virtual Persona Persona { get; set; }

        public string Institucion { get; set; }


    }
}
