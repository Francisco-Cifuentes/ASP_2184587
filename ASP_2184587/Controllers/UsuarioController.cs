using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
//importando los modelos de la base de datos
using ASP_2184587.Models;
using System.Web.Security;

namespace ASP_2184587.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [Authorize]
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.usuario.ToList());
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                using (var db = new inventarioEntities())
                {
                    usuario.password = UsuarioController.HashSHA1(usuario.password);
                    db.usuario.Add(usuario);
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
        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        [Authorize]
        public ActionResult Details (int id)
        {
            using (var db = new inventarioEntities())
            {
                var findUser = db.usuario.Find(id);
                return View(findUser);
            }
        }
        [Authorize]
        public ActionResult Edit (int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();                    
                    return View(findUser);
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
        public ActionResult Edit(usuario editUser)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    usuario user = db.usuario.Find(editUser.id);

                    user.nombre = editUser.nombre;
                    user.apellido = editUser.apellido;
                    user.email = editUser.email;
                    user.fecha_nacimiento = editUser.fecha_nacimiento;
                    user.password = HashSHA1(editUser.password);

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

        public ActionResult Delete (int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var findUser = db.usuario.Find(id);
                    db.usuario.Remove(findUser);
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

        public ActionResult Login(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string user, string pass)
        {
            string Pass_Encrip = UsuarioController.HashSHA1(pass);
            try
            {
                using (var db = new inventarioEntities())
                {
                    var userLogin = db.usuario.FirstOrDefault(e => e.email == user && e.password == Pass_Encrip);
                    if (userLogin != null)
                    {
                        FormsAuthentication.SetAuthCookie(userLogin.email, true);
                        Session["User"] = userLogin;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Login("Verifique sus credenciales");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error - Datos ingresados de manera erronea");
                return View();
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}