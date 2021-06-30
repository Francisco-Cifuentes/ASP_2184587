using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_2184587.Models;

namespace ASP_2184587.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        // GET: Cliente
        [Authorize]
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.cliente.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                using (var db = new inventarioEntities())
                {
                    db.cliente.Add(cliente);
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
                var findClient = db.cliente.Find(id);
                return View(findClient);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    cliente findClient = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findClient);
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
        public ActionResult Edit(cliente editClient)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    cliente user = db.cliente.Find(editClient.id);

                    user.nombre = editClient.nombre;
                    user.documento = editClient.documento;
                    user.email = editClient.email;

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
                    var findClient = db.cliente.Find(id);
                    db.cliente.Remove(findClient);
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