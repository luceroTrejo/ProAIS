using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIS.Controllers
{
    public class LoginController : Controller
    {
        private MisistemaEntities db = new MisistemaEntities();
        public object Models { get; private set; }

        // GET: LoginAIS
        public ActionResult Login(string User, string Pass)
        {
            //return View
            try
            {
                using (MisistemaEntities db = new MisistemaEntities()
                  )
                {
                    var oUser = (from d in db.Usuarios
                                 where d.usuario == User.Trim() && d.contraseña == Pass.Trim()
                                 select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o cantraseña Invalida";
                        return View();
                    }
                    Session["User"] = oUser;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                return View();

            }
        }


        public ActionResult RegistrarUsuario()
        {
            return View();
        }


        //public ActionResult RegistrarUsuario(string nombre, string app, string apm,  string usuario, string contraseña)
        [HttpPost]
        public ActionResult RegistrarUsuario(Usuarios user)
        {

            try
            {
                var defaultRol = db.Roles.Where(x => x.Nombre_roles == "Admin").First().Id_rol;
                Usuarios nuevoUsuario = new Usuarios();

                nuevoUsuario.nombre = user.nombre;
                nuevoUsuario.app = user.app;
                nuevoUsuario.apm = user.apm;

                var userBytes = System.Text.Encoding.UTF8.GetBytes(user.usuario);
                nuevoUsuario.usuario = Convert.ToBase64String(userBytes).ToString();


                var passwordBytes = System.Text.Encoding.UTF8.GetBytes(user.contraseña);
                nuevoUsuario.contraseña = Convert.ToBase64String(passwordBytes).ToString();

                nuevoUsuario.Id_rol = defaultRol;

                db.Usuarios.Add(nuevoUsuario);
                db.SaveChanges();

                return RedirectToAction("Login");

            }
            catch (Exception)
            {

                return View();
            }


        }
    }
}