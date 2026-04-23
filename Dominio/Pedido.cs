using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pedido 
    {
        
            public int Id { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un cliente")]
        [Range(1, int.MaxValue, ErrorMessage = "Por favor, seleccione un cliente de la lista")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
            [DataType(DataType.Date)]
            public DateTime Fecha { get; set; }

            [Required(ErrorMessage = "El total es obligatorio")]
            [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
            public decimal Total { get; set; }

            [Required(ErrorMessage = "El estado es obligatorio")]
            public string Estado { get; set; }

        public List<DetallePedido> DetallesPedido { get; set; } = new DetallePedido(); 
    }
    }

