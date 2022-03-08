using Utilitarios;
using Data;
using System.Threading.Tasks;
using Utilitarios.Entrada;

namespace Logica
{
    public class LRegistroCliente
    {
        public async Task registro(RegistroClienteRequest registro) //S
        {
            await new DaoCliente().inserCliente(registro);
        }

        public Cliente validarUsuario(RegistroClienteRequest cliente) //S
        {
            return new DaoCliente().validarExistencia(cliente);
        }

        public Cliente validarCorreo(RegistroClienteRequest cliente) //S
        {
            return new DaoCliente().validarExistenciaCorreos(cliente);
        }
    }
}
