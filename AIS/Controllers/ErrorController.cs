using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIS.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult UnauthotizedOperation(string operacion, string modulo, string msjErrorException)
        {
            ViewBag.operacion = operacion;
            ViewBag.modulo = modulo;
            ViewBag.msjErrorException = msjErrorException;
            return View("VistaError");
        }
    }
}