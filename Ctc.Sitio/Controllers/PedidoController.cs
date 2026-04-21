using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Ctc.Sitio.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            
            var clientes = ClienteService.Instancia.TraerTodos();
            ViewBag.Clientes = clientes;

            return View();
        }
    }
}
