using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto
    {
        public int Id_producto { get; set; }
        public  string Codigo { get; set; }

        public string Nombre { get; set; }

        public float  Precio { get; set; }
    }
}
