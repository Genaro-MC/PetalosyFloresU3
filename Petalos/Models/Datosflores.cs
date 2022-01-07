using System;
using System.Collections.Generic;

#nullable disable

namespace Petalos.Models
{
    public partial class DatosFloressssss
    {
        public DatosFloressssss()
        {
            ImagenesFlores = new HashSet<Imagenesflores>();
        }

        public uint Idflor { get; set; }
        public string NombreCientifico { get; set; }
        public string NombreComun { get; set; }
        public string Origen { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Imagenesflores> ImagenesFlores { get; set; }
    }
}
