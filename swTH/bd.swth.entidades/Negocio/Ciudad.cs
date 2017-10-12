namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Ciudad
    {
        [Key]
        public int IdCiudad { get; set; }

        public string Nombre { get; set; }

        [NotMapped]
        public int IdPais { get; set; }
        //Propiedades Virtuales Referencias a otras clases

        [Display(Name = "Provincia:")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0} ")]
        public int IdProvincia { get; set; }
        public virtual Provincia Provincia { get; set; }

       

        public virtual ICollection<Sucursal> Sucursal { get; set; }

        public virtual ICollection<Parroquia> Parroquia { get; set; }

        public virtual ICollection<ItinerarioViatico> ItinerarioViatico { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }


    }
}
