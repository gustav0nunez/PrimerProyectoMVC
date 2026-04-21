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
            if (p == null) throw new Exception("El objeto Pedido no puede ser nulo");

            Cliente existente = Mapper.ObtenerClientePorMail(c.Email);

            if (existente != null)
            {
                throw new Exception("Ya existe un cliente registrado con ese email");

            }
            Mapper.Guardar(c);
        }
    }
}
