namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EscalaGrados
    {
        [Key]
        public int IdEscalaGrados { get; set; }
        
        public int? Grado { get; set; }
        
        public decimal? Remuneracion { get; set; }

        public string Nombre { get; set; }
        
        public int? IdGrupoOcupacional { get; set; }
        public virtual GrupoOcupacional GrupoOcupacional { get; set; }

        public virtual ICollection<IndiceOcupacional> IndiceOcupacional { get; set; }

       
    }
}
