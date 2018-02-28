using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class SubcategoryInstitution
    {
        public string IdSubCategoryInstitution { get; set; }
        public string IdSubcategory { get; set; }
        public string IdInstitution { get; set; }

        public virtual Institution IdInstitutionNavigation { get; set; }
    }
}
