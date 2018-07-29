using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Institution
    {
        public Institution()
        {
            SubcategoryInstitution = new HashSet<SubcategoryInstitution>();
        }

        public string IdInstitution { get; set; }
        public string Name { get; set; }
        public string LinkFacebook { get; set; }
        public string LinkTwitter { get; set; }

        public virtual ICollection<SubcategoryInstitution> SubcategoryInstitution { get; set; }
    }
}
