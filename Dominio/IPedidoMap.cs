using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public interface IPedidoMap
    {
        void Guardar(Pedido p);
        List<Pedido> ObtenerTodos();
        Pedido ObtenerPorId(int id);
        void Eliminar(int id);
    }
}
