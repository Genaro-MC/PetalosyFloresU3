using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


using Petalos.Models;
using Petalos.Areas.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Petalos.Controllers
{
    [Area("Admin")]
    public class FloresController : Controller
    {
        public floresContext Context { get; }
        public IWebHostEnvironment Host { get; }

        public FloresController(floresContext context, IWebHostEnvironment host)
        {
            Context = context;
            Host = host;
        }
        public IActionResult Index()
        {
            var f = Context.DatosFloressssss.OrderBy(x => x.Nombre);
            return View(f);
        }
        [HttpGet]
        public IActionResult AgregarFlor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AgregarFlor(FloresCrud ViewModelFlores)
        {
            if (string.IsNullOrWhiteSpace(ViewModelFlores.Flor.Nombre))
            {
                ModelState.AddModelError("", "El nombre de la flor está vacío");
                return View(ViewModelFlores);
            }
            if (string.IsNullOrWhiteSpace(ViewModelFlores.Flor.NombreCientifico))
            {
                ModelState.AddModelError("", "El nombre cientifico de la flor está vacío");
                return View(ViewModelFlores);
            }
            if (string.IsNullOrWhiteSpace(ViewModelFlores.Flor.Origen))
            {
                ModelState.AddModelError("", "El origen de la flor está vacío");
                return View(ViewModelFlores);
            }
            if (string.IsNullOrWhiteSpace(ViewModelFlores.Flor.Descripcion))
            {
                ModelState.AddModelError("", "La descripción de la flor está vacía");
                return View(ViewModelFlores);
            }
            if (Context.DatosFloressssss.Any(x => x.Nombre == ViewModelFlores.Flor.Nombre))
            {
                ModelState.AddModelError("", "El nombre de la flor ya existe");
                return View(ViewModelFlores);
            }
            if (Context.DatosFloressssss.Any(x => x.NombreCientifico == ViewModelFlores.Flor.NombreCientifico))
            {
                ModelState.AddModelError("", "El nombre científico de la flor ya existe");
                return View(ViewModelFlores);
            }
            if (Context.DatosFloressssss.Any(x => x.NombreComun == ViewModelFlores.Flor.NombreComun))
            {
                ModelState.AddModelError("", "El nombre común de la flor ya existe");
                return View(ViewModelFlores);
            }
            Context.Add(ViewModelFlores.Flor);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AgregarImagenes(int id)
        {
            
            var f = Context.DatosFloressssss.Include(x => x.ImagenesFlores).FirstOrDefault(x => x.Idflor == id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            FloresCrud vm = new();
            vm.ImagenId = Context.Imagenesflores.FirstOrDefault(x => x.IdFlor == f.Idflor);
            vm.Flor = f;
          
            vm.Flores = Context.DatosFloressssss.OrderBy(x => x.Nombre);
          
            return View(vm);

             
          
        }
        [HttpPost]
        public IActionResult AgregarImagenes(FloresCrud ViewModelFlores,IFormFile foto)
        {
            if (foto==null)
            {    
                ViewModelFlores.Flores = Context.DatosFloressssss.OrderBy(x => x.Nombre);
                ModelState.AddModelError("", "No hay ninguna fotografía");
                return View(ViewModelFlores);
            }
            if (foto != null)
            {
                if (foto.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo se permite la carga de archivos .jpg");
                    return View(ViewModelFlores);

                }
                if (foto.Length > 1024 * 1024 * 5)
                {
                    ModelState.AddModelError("", "No se permite la carga de archivos mayores a 5MB");
                    return View(ViewModelFlores);
                }
            }
            ViewModelFlores.ImagenId.NombreImagen = foto.FileName;
            Context.Add(ViewModelFlores.ImagenId);
           
            Context.SaveChanges();
            if (foto != null)
            {
                String var = "Hola a todo mundo";
                int tam_var = var.Length;
                String Var_Sub = var.Substring((tam_var - 2), 2);
                var path = Host.WebRootPath + "/images/" + ViewModelFlores.ImagenId.NombreImagen /*+ ".jpg"*/;
                FileStream fs = new FileStream(path, FileMode.Create);
                foto.CopyTo(fs);
                fs.Close();
            }
            return RedirectToAction("AgregarImagenes");
          
        }
        [HttpGet]
        public IActionResult EditarFlor(int id)
        {
            var f = Context.DatosFloressssss.FirstOrDefault(x => x.Idflor == id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }

            FloresCrud vm = new FloresCrud
            {
                Flor = f
            };

            return View(vm);

        }
        [HttpPost]
        public IActionResult EditarFlor(FloresCrud ViewModelFlores)
        {

            var f = Context.DatosFloressssss.FirstOrDefault(x => x.Idflor == ViewModelFlores.Flor.Idflor);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            if (string.IsNullOrWhiteSpace(ViewModelFlores.Flor.Nombre))
            {
                ModelState.AddModelError("", "El nombre de la flor está vacío");
                return View(ViewModelFlores);
            }
            if (string.IsNullOrWhiteSpace(ViewModelFlores.Flor.NombreCientifico))
            {
                ModelState.AddModelError("", "El nombre científico de la flor está vacío");
                return View(ViewModelFlores);
            }
            if (string.IsNullOrWhiteSpace(ViewModelFlores.Flor.Origen))
            {
                ModelState.AddModelError("", "El origen de la flor está vacío");
                return View(ViewModelFlores);
            }
            if (string.IsNullOrWhiteSpace(ViewModelFlores.Flor.Descripcion))
            {
                ModelState.AddModelError("", "La descripción de la flor está vacía");
                return View(ViewModelFlores);
            }
            if (Context.DatosFloressssss.Any(x => x.Nombre == ViewModelFlores.Flor.Nombre && x.Idflor != ViewModelFlores.Flor.Idflor))
            {
                ModelState.AddModelError("", "El nombre de la flor ya existe");
                return View(ViewModelFlores);
            }
            if (Context.DatosFloressssss.Any(x => x.NombreCientifico == ViewModelFlores.Flor.NombreCientifico && x.Idflor != ViewModelFlores.Flor.Idflor))
            {
                ModelState.AddModelError("", "El nombre científico de la flor ya existe");
                return View(ViewModelFlores);
            }
            if (Context.DatosFloressssss.Any(x => x.NombreComun == ViewModelFlores.Flor.NombreComun && x.Idflor != ViewModelFlores.Flor.Idflor))
            {
                ModelState.AddModelError("", "El nombre común de la flor ya existe");
                return View(ViewModelFlores);
            }
            f.NombreCientifico = ViewModelFlores.Flor.NombreCientifico;
            f.NombreComun = ViewModelFlores.Flor.NombreComun;
            f.Origen = ViewModelFlores.Flor.Origen;
            f.Descripcion = ViewModelFlores.Flor.Descripcion;
            f.Nombre = ViewModelFlores.Flor.Nombre;
            Context.Update(f);
            Context.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult EliminarFlor(int id)
        {
            var f = Context.DatosFloressssss.FirstOrDefault(x => x.Idflor == id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            return View(f);
        }
        [HttpGet]
        public IActionResult EliminarImagen(int id)
        {
            var img = Context.Imagenesflores.FirstOrDefault(x => x.IdImagen == id);
            if (img == null)
            {
                return RedirectToAction("Index");
            }
            return View(img);
        }
        [HttpPost]
        public IActionResult EliminarImagen(Imagenesflores imagenF)
        {
            var img = Context.Imagenesflores.FirstOrDefault(x => x.IdImagen == imagenF.IdImagen);

            if (img == null)
            {
                ModelState.AddModelError("", $"La fotografía {imagenF.NombreImagen} ha sido eliminada, o no existe");
                return View(imagenF);
            }
            Context.Remove(img);
            Context.SaveChanges();
            string path = Host.WebRootPath + "/images/" + img.NombreImagen;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EliminarFlor(DatosFloressssss flor)
        {
            var f = Context.DatosFloressssss.FirstOrDefault(x => x.Idflor == flor.Idflor);

            if (f == null)
            {
                ModelState.AddModelError("", "La flor no existe o ya ha sido eliminada");
                return View(flor);
            }
            Context.Remove(f);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

    }


}