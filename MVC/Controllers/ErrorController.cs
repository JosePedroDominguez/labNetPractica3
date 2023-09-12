using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ErrorController : Controller
    {

        public ActionResult Index(Exception ex)
        {
            var exceptionView = new ExceptionsView
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace
            };

            // Puedes redirigir a la vista de error y pasar el objeto exceptionView
            return View("Index", exceptionView);
        }
    }
}
