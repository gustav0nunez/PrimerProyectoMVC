using Microsoft.AspNetCore.Mvc;

namespace Ctc.Sitio.Controllers
{
    public class NoticiasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ultimas()
        {
            return View();
        }
    }
}
