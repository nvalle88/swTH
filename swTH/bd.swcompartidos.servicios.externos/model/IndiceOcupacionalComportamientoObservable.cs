using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class IndiceOcupacionalComportamientoObservable
    {
        public int IdIndiceOcupacionalComportamientoObservable { get; set; }
        public int IdComportamientoObservable { get; set; }
        public int IdIndiceOcupacional { get; set; }

        public virtual ComportamientoObservable IdComportamientoObservableNavigation { get; set; }
        public virtual IndiceOcupacional IdIndiceOcupacionalNavigation { get; set; }
    }
}
