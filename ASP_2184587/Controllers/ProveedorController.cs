using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_2184587.Models;


namespace ASP_2184587.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
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
            catch (Exception ex)
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
    }
}