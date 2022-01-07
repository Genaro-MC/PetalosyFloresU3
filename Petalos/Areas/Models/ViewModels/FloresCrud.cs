using Microsoft.AspNetCore.Http;
using Petalos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petalos.Areas.Models.ViewModels
{
    public class FloresCrud
    {
        public DatosFloressssss Flor { get; set; }
        public IEnumerable<DatosFloressssss> Flores { get; set; }
        public Imagenesflores ImagenId { get; set; }
       

    }
}
