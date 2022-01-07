using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petalos.Models;
using Petalos.Models.ViewModels;

namespace Petalos.Controllers
{
    public class HomeController : Controller
    {
        public floresContext Context { get; set; }
        public HomeController(floresContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {

            var f = Context.DatosFloressssss.OrderBy(x => x.Nombre);
            return View(f);
        }
        [Route("informacion/{nombre}")]
        public IActionResult Flor(string nombre)
        {
            var f = Context.DatosFloressssss.Include(x=>x.ImagenesFlores).FirstOrDefault(x => x.Nombre == nombre.Replace("-", " "));
            if (f==null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                FloresViewModel vm = new();
                vm.FlorUnica = f;
                Random rnd = new();
                vm.CuatroFlores=Context.DatosFloressssss
                   
                    .Where(x => x.Idflor != f.Idflor)
                    .ToList()
                    .OrderBy(x => rnd.Next())
                    .Take(4)
                    .Select(x => new DatosFloressssss { Idflor = x.Idflor, Nombre = x.Nombre });
                return View(vm);
            }
           
        }
    }
}