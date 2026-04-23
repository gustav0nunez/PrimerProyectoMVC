using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Ctc.Sitio.Controllers
{
    public class PedidosController : Controller
    {
       
            public IActionResult Index()
        {
           
            var listaPedidos = PedidoService.Instancia.ObtenerTodos();
            return View(listaPedidos);
        }
        

        [HttpGet]
        public IActionResult Crear()
        {
            
            List<Cliente>clientes = ClienteService.Instancia.TraerTodos();
            ViewBag.ListadoClientes = clientes;
            return View(); 
        }


        [HttpPost]

        public IActionResult AgregarItem (Pedido pedido)
        {
            List<Cliente> clientes = ClienteService.Instancia.TraerTodos();
            ViewBag.DetallesPedido.add(new DetallePedido());
            return View("Crear", pedido);
        }
      /*  public IActionResult Crear(Pedido p)
        {
            
            if (!ModelState.IsValid)
            {
                
                ViewBag.Clientes = ClienteService.Instancia.TraerTodos();
                return View(p);
            }

            try
            {
                
                PedidoService.Instancia.GuardarPedido(p);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.Clientes = ClienteService.Instancia.TraerTodos();
                return View(p);
            }
        }
        */

        public IActionResult QuitarItem(Pedido pedido, int indice)
        {
            if (indice >= 0 && indice < pedido.DetallesPedido.Count)
            {
                pedido.DetallesPedido.RemoveAt(indice);
            }
            return View("Crear", pedido);
        }

    }

   

  
}
