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
    }
    }
