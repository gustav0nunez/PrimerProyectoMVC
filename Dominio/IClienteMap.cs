using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    
    public interface IClienteMap
    {
        void Guardar(Cliente c);
        void Modificar(Cliente c);
        void Eliminar(int id);
        Cliente ObtenerPorId(int id);
        List<Cliente> ObtenerTodos();

        Cliente ObtenerClientePorMail(string mail); 
    }
}
