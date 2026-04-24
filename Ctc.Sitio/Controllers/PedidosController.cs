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
           
            ViewBag.Clientes = ClienteService.Instancia.TraerTodos();
            ViewBag.Productos = ProductoService.Instancia.ObtenerTodos();

            Pedido pedidoNuevo = new Pedido();
            pedidoNuevo.Fecha = DateTime.Now;

            pedidoNuevo.DetallesPedido = new List<DetallePedido>();

            return View(pedidoNuevo);
        }


        [HttpPost]
        public IActionResult Crear(Pedido p)
        {
            p.Estado = "Pendiente";
            ModelState.Remove("Estado");

            if (p.IdCliente <= 0 || p.Total <= 0 || p.DetallesPedido == null || p.DetallesPedido.Count == 0)
            {
                ModelState.AddModelError("", "Debes elegir un cliente, un total válido y al menos un producto.");
                ViewBag.Clientes = ClienteService.Instancia.TraerTodos();
                ViewBag.Productos = ProductoService.Instancia.ObtenerTodos();
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
                ViewBag.Productos = ProductoService.Instancia.ObtenerTodos();
                return View(p);
            }
        }

        [HttpPost]
        public IActionResult AgregarItem(Pedido pedido)
        {

            List<Cliente> clientes = ClienteService.Instancia.TraerTodos();
            List<Producto> productos = ProductoService.Instancia.ObtenerTodos();
            ViewBag.Clientes = clientes;
            ViewBag.Productos = productos;


            if (pedido.DetallesPedido == null)
            {
                pedido.DetallesPedido = new List<DetallePedido>();
            }
            pedido.DetallesPedido.Add(new DetallePedido());
            ModelState.Clear();
            return View("Crear", pedido);
        }

        [HttpPost]
        public IActionResult QuitarItem(Pedido pedido, int indice)
        {
            ViewBag.Clientes = ClienteService.Instancia.TraerTodos();
            ViewBag.Productos = ProductoService.Instancia.ObtenerTodos();


            if (indice >= 0 && indice < pedido.DetallesPedido.Count)
            {
                pedido.DetallesPedido.RemoveAt(indice);
            }
            ModelState.Clear();
            return View("Crear", pedido);
        }
        
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            try
            {
                PedidoService.Instancia.Eliminar(id);
            }
            catch (Exception ex)
            {

                TempData["Error"] = "No se pudo eliminar el pedido: " + ex.Message;
            }

            return RedirectToAction("Index");
        }


    }

   

  
}
