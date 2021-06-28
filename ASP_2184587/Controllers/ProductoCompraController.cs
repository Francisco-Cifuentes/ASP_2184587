using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_2184587.Models;

namespace ASP_2184587.Controllers
{
    public class ProductoCompraController : Controller
    {
        // GET: ProductoCompra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.producto_compra.ToList());
            }
        }
         public static string NombreProducto(int? idProducto)
        {
            using (var db = new inventarioEntities())
            {
                return db.producto.Find(idProducto).nombre;
            }
        }

        public ActionResult ListarCompra()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.compra.ToList());
            }
        }
        public ActionResult ListarProducto()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.producto.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_compra ProductoCompra)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                using (var db = new inventarioEntities())
                {
                    db.producto_compra.Add(ProductoCompra);
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
                var finProCom = db.producto_compra.Find(id);
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto_compra findProCom = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findProCom);
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

        public ActionResult Edit (producto_compra editProCom)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto_compra ProCom = db.producto_compra.Find(editProCom.id);

                    ProCom.id_compra = editProCom.id_compra;
                    ProCom.id_producto = editProCom.id_producto;
                    ProCom.cantidad = editProCom.cantidad;
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

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var findProCom = db.producto_compra.Find(id);
                    db.producto_compra.Remove(findProCom);
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