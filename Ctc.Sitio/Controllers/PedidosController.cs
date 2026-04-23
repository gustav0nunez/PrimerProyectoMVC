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
            // 1. Cargamos los clientes reales de tu base de datos
            ViewBag.Clientes = ClienteService.Instancia.TraerTodos();

            // 2. PRODUCTOS MOCKEADOS (DE MENTIRA)
            // Inventamos una lista rápida en memoria para que la vista no explote
            var productosFalsos = new List<dynamic>
    {
        new { Id = 1, Nombre = "Monitor Samsung 24 pulgadas" },
        new { Id = 2, Nombre = "Teclado Mecánico Redragon" },
        new { Id = 3, Nombre = "Mouse Logitech G203" },
        new { Id = 4, Nombre = "Auriculares HyperX" }
    };

            // Le pasamos esta lista inventada a la vista
            ViewBag.Productos = productosFalsos;

            // 3. Le mandamos un Pedido vacío para que la tabla de detalles no sea "null"
            Pedido pedidoNuevo = new Pedido();
            pedidoNuevo.DetallesPedido = new List<DetallePedido>();

            return View(pedidoNuevo);
        }


        [HttpPost]
        public IActionResult AgregarItem(Pedido pedido)
        {
            
            List<Cliente> clientes = ClienteService.Instancia.TraerTodos();
            ViewBag.Clientes = clientes;

            
            if (pedido.DetallesPedido == null)
            {
                pedido.DetallesPedido = new List<DetallePedido>();
            }
            pedido.DetallesPedido.Add(new DetallePedido());
            ModelState.Clear();
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
