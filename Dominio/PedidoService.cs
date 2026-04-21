using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class PedidoService
    {
        public static IPedidoMap Mapper { get; set; }

        private static PedidoService instancia;
        public static PedidoService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new PedidoService();
                }
                return instancia;
            }
        }

        
        public void GuardarPedido(Pedido p)
        {
            if (p.Total <= 0) throw new Exception("El total debe ser mayor a cero");
            p.Fecha = DateTime.Now;
            Mapper.Guardar(p);
        }

       
        public List<Pedido> ObtenerTodos()
        {
            return Mapper.ObtenerTodos();
        }

       
        public Pedido ObtenerPorId(int id)
        {
            if (id <= 0) throw new Exception("ID de pedido inválido.");
            return Mapper.ObtenerPorId(id);
        }

      
        public void Eliminar(int id)
        {
            if (id <= 0) throw new Exception("ID de pedido inválido.");
            Mapper.Eliminar(id);
        }
    }
}