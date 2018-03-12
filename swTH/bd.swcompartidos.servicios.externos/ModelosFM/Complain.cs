using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class Complain
    {
        public string IdComplain { get; set; }
        public int IdUser { get; set; }
        public string IdSubcategory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
    }
}
