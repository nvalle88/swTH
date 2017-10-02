namespace bd.swth.entidades.Negocio
{
    using System.ComponentModel.DataAnnotations;

    public partial class DocumentoInformacionInstitucional
    {
        [Key]
        public int IdDocumentoInformacionInstitucional { get; set; }

        public string Nombre { get; set; }

      
        public string Url { get; set; }
    }
}
