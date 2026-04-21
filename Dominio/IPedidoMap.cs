using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public interface IPedidoMap
    {
        List<Pedido> ObtenerTodos();
        void Guardar(Pedido p);
    }
}
