using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Ctc.Sitio.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            var lista = ClienteService.Instancia.TraerTodos();
            return View(lista); 
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Cliente nuevoCliente)
        {
            if (!ModelState.IsValid)
            {
                return View(nuevoCliente); 
            }
            try
            {
                
                ClienteService.Instancia.GuardarCliente(nuevoCliente);

                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                
                ViewBag.MensajeError = ex.Message;
                return View(nuevoCliente);
            }
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            try
            {
                ClienteService.Instancia.EliminarCliente(id);
            }
            catch (Exception ex)
            {
                
                TempData["Error"] = "No se pudo eliminar: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        public IActionResult Detalles(int id)
        {
            var encontrado = ClienteService.Instancia.ObtenerPorId(id);

            if (encontrado == null)
            {
                return RedirectToAction("Index");
            }

            
            return View(encontrado);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            
            var cliente = ClienteService.Instancia.ObtenerPorId(id);

            if (cliente == null) return RedirectToAction("Index");

            
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Editar(Cliente clienteActualizado)
        {
            if (!ModelState.IsValid) return View(clienteActualizado);

            try
            {
                ClienteService.Instancia.ModificarCliente(clienteActualizado);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(clienteActualizado);
            }
        }

    }
}
