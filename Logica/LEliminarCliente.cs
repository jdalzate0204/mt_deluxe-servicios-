using Utilitarios;
using Data;
using System.Threading.Tasks;

namespace Logica
{
    public class LEliminarCliente
    {
        public async Task<Cascaron> eliminar(Cliente cliente) //S
        {
            Cascaron cascaron = new Cascaron();
            await new DaoCliente().eliminarCliente(cliente);
            cascaron.Cliente = null;
            cascaron.Url = "loginCliente.aspx";
            return cascaron;
        }

        public async Task eliminarToken(ClienteTokenLogin token) //S
        {
            await new DaoCliente().eliminarToken(token);
        }

        public async Task eliminarRegistro(Cliente cliente) //S
        {
           
            await new DaoCliente().eliminarRegistro(cliente);
            
        }
    }
}
