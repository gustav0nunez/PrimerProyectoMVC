using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Dominio
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public string Direccion { get; set; }
    }
}
