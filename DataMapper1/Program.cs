using Dominio;
using Persistencia; 

ClienteService.Mapper = new ClienteMaper();


Cliente cli = new Cliente();
cli.Nombre = "Juan";
cli.Direccion = "Avenida 823";
cli.Email = "test_nuevo@mail.com";
cli.FechaNacimiento = Convert.ToDateTime("05/02/1999");


try
{
    ClienteService.Instancia.GuardarCliente(cli);
    Console.WriteLine("El cliente: " + cli.Nombre + " se guardo con el ID: " + cli.Id);

}
catch (Exception ex)
{
    Console.WriteLine("Error de validación: " + ex.Message);
}