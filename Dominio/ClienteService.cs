using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ClienteService
    {
        public static IClienteMap Mapper { get; set; }


        #region Singleton
        private static ClienteService instancia; 
        public static ClienteService Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ClienteService();
                }
                return instancia;
            }
        }

        private ClienteService() { 
        }

        #endregion

        public void GuardarCliente(Cliente c)
        {
            if (c == null) throw new Exception("El objeto cliente no puede ser nulo");

            if (string.IsNullOrEmpty(c.Nombre))
            {
                throw new Exception("El nombre es obligatorio");
            }

            if (string.IsNullOrEmpty(c.Direccion))
            {
                throw new Exception("La dirección es obligatoria");
            }

            if (c.FechaNacimiento == null)
            {
                throw new Exception("La fecha es obligatoria");
            }


            Cliente existente = Mapper.ObtenerClientePorMail(c.Email);   

            if (existente != null)
            {
                throw new Exception("Ya existe un cliente registrado con ese email");
             
            }
            Mapper.Guardar(c);
        }

        public List<Cliente> TraerTodos()
        {
            return Mapper.ObtenerTodos();
        }

        public void EliminarCliente(int id)
        {
            Mapper.Eliminar(id);
        }

        public void DetalleCliente(int id)
        {
            Mapper.ObtenerPorId(id);
        }

        public Cliente ObtenerPorId(int id)
        {
            return Mapper.ObtenerPorId(id);
        }

        public void ModificarCliente(Cliente c)
        {
            
            Mapper.Modificar(c);
        }
        /*hola*/
    }
}
