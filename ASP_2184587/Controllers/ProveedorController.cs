using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_2184587.Models;
using System.IO;


namespace ASP_2184587.Controllers
{
    [Authorize]
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        [Authorize]
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.proveedor.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(proveedor proveedor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                using (var db = new inventarioEntities())
                {
                    db.proveedor.Add(proveedor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error - Datos ingresados de manera erronea");
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities())
            {
                var findProv = db.proveedor.Find(id);
                return View(findProv);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    proveedor findProv = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findProv);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(proveedor editProv)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    proveedor prov = db.proveedor.Find(editProv.id);

                    prov.nombre = editProv.nombre;
                    prov.direccion = editProv.direccion;
                    prov.telefono = editProv.telefono;
                    prov.nombre_contacto = editProv.nombre_contacto;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error - Datos ingresados de manera erronea");
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var findProv = db.proveedor.Find(id);
                    db.proveedor.Remove(findProv);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
            }
        }

        public ActionResult UploadCSV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadCSV(HttpPostedFileBase ArchivoFormulario)
        {
            //Guardar la ruta
            string FilePath = string.Empty;

            //Saber si llego el archivo
            if (ArchivoFormulario != null)
            {
                //Ruta de la carpeta que carga el archivo
                string path = Server.MapPath("~/Uploads/");

                //Verificar si la ruta de la carpeta existe
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Obtener el nombre del archivo
                FilePath = path + Path.GetFileName(ArchivoFormulario.FileName);
                //Obtener la extension
                string extension = Path.GetExtension(ArchivoFormulario.FileName);
                //Guardar el archivo
                ArchivoFormulario.SaveAs(FilePath);

                string CSVData = System.IO.File.ReadAllText(FilePath);
                foreach (string row in CSVData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        var NuevoProveedor = new proveedor
                        {
                            nombre = row.Split(';')[0],
                            nombre_contacto = row.Split(';')[1],
                            direccion = row.Split(';')[2],
                            telefono = row.Split(';')[3]
                        };
                        using (var db = new inventarioEntities())
                        {
                            db.proveedor.Add(NuevoProveedor);
                            db.SaveChanges();
                        }
                    }
                }

            }
            return RedirectToAction("Index");
        }
    }
}