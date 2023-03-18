using AIS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIS.Filters
{
    public class VerificaSesion : ActionFilterAttribute
    {
        private Usuarios oUsuario;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                base.OnActionExecuted(filterContext);
                oUsuario = (Usuarios)HttpContext.Current.Session["User"];
                if (oUsuario == null)
                {
                    if (filterContext.Controller is LoginController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("~/LoginAIS/LoginAIS");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}