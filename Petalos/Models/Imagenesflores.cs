using System;
using System.Collections.Generic;

#nullable disable

namespace Petalos.Models
{
    public partial class Imagenesflores
    {
        public uint IdImagen { get; set; }
        public string NombreImagen { get; set; }
        public uint IdFlor { get; set; }

        public virtual DatosFloressssss IdFlorNavigation { get; set; }
    }
}
