using Data;
using System.Threading.Tasks;
using Utilitarios;

namespace Logica
{
    public class LModificarCliente
    {
        public async Task<Cliente> mostrar(int idCliente) //S
        {
            return await new DaoCliente().mostrarRegistro(idCliente);
        }

        public async Task<Cascaron> modificar(Cliente cliente) //S
        {
            Cascaron cascaron = new Cascaron();
            cascaron.Cliente = cliente;
            if (cascaron.Cliente != null)
            {
                await new DaoCliente().modificarCliente(cliente);
                cascaron.Url = "modificarCliente.aspx";
            }
            return cascaron;
        }
    }
}
