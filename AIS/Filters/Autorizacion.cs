using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIS.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthoriceUser : AuthorizeAttribute
    {
        private Usuarios oUsuario;
        private MisistemaEntities db = new MisistemaEntities();
        private int idOperacion;

        public AuthoriceUser(int idOperacion = 0)
        {
            this.idOperacion = idOperacion;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String nombreoperacion = "";
            String nombremodulo = "";
            try
            {
                oUsuario = (Usuarios)HttpContext.Current.Session["User"];
                var lstMisOperaciones = from m in db.RolOperaciones
                                        where m.Id_rol == oUsuario.Id_rol
                                        && m.Id_operacion == idOperacion
                                        select m;
                if (lstMisOperaciones.ToList().Count < 1)
                {
                    var oOperacion = db.Operaciones.Find(idOperacion);
                    int idModulo = oOperacion.Id;
                    nombreoperacion = GetNombredeOperacion(idOperacion);
                    nombremodulo = GetNombredemodulo(idModulo);
                    filterContext.Result = new RedirectResult("~/Error/UnauthotizedOperation?operation=" + nombreoperacion);
                }
            }
            catch (Exception)
            {

                filterContext.Result = new RedirectResult("~/Error/UnauthotizedOperation?operation=" + nombreoperacion);
            }

        }
        public string GetNombredeOperacion(int idOperacion)
        {
            var ope = from op in db.Operaciones
                      where op.Id == idOperacion
                      select op.Nombre;
            string nombreoperaciones;
            try
            {
                nombreoperaciones = ope.First();
            }
            catch (Exception)
            {

                nombreoperaciones = " ";
            }
            return nombreoperaciones;
        }
        public string GetNombredemodulo(int idmodulo)
        {
            var ope = from op in db.Modulo
                      where op.Id_modulo == idmodulo
                      select op.Nombre_modulo;
            string nombremod;
            try
            {
                nombremod = ope.First();
            }
            catch (Exception)
            {

                nombremod = " ";
            }
            return nombremod;
        }
    }
}