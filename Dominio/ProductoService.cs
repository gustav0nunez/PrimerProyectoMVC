using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ProductoService
    {
        public static IProductoMap Mapper { get; set; }

        private static ProductoService instancia;
        public static ProductoService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ProductoService();
                }
                return instancia;
            }
        }

        public List<Producto> ObtenerTodos()
        {
            return Mapper.ObtenerTodos();
        }
    }
}
