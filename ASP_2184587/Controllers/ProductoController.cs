using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_2184587.Models;

namespace ASP_2184587.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        // GET: Producto
        [Authorize]
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.producto.ToList());
            }
        }

        public static string NombreProveedor(int ? idProveedor)
        {
            using (var db = new inventarioEntities())
            {
                return db.proveedor.Find(idProveedor).nombre;
            }
        }

        public ActionResult ListarProveedores()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.proveedor.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto producto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                using (var db = new inventarioEntities())
                {                    
                    db.producto.Add(producto);
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
                var findProd = db.producto.Find(id);
                return View(findProd);
            }
        }

        public ActionResult Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto findProd = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(findProd);
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
        public ActionResult Edit(producto editProd)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto Prod = db.producto.Find(editProd.id);

                    Prod.nombre = editProd.nombre;
                    Prod.percio_unitario = editProd.percio_unitario;
                    Prod.descripcion = editProd.descripcion;
                    Prod.cantidad = editProd.cantidad;
                    Prod.id_proveedor = editProd.id_proveedor;

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
                    var findProd = db.producto.Find(id);
                    db.producto.Remove(findProd);
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


        public ActionResult Reporte()
        {
            var db = new inventarioEntities();            
            var query = from tabProveedor in db.proveedor
                        join tabProducto in db.producto on tabProveedor.id equals tabProducto.id_proveedor
                        select new Reporte
                        {
                            Nombre_Porv = tabProveedor.nombre,
                            Telefono_Prov = tabProveedor.telefono,
                            Direccion_Prov = tabProveedor.direccion,
                            Nombre_Pord = tabProducto.nombre,
                            Precio_Pord = tabProducto.percio_unitario
                        };
            return View(query);            
        }
    }
}