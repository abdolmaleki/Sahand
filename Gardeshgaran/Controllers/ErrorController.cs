
using System.Web.Mvc;

namespace Gardeshgaran.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult HTTP404()
        {
            return View();
        }
        public ActionResult HTTP501()
        {
            return View();
        }
        public ActionResult HTTP500(string ErrorMessage)
        {
            return View();
        }
    }
}