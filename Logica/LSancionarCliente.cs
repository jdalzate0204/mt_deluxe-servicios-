using Utilitarios;
using Data;

namespace Logica
{
    public class LSancionarCliente
    {
        public Notificacion buscaridN (double idCl)//S
        {
            return new DaoAdministrador().buscaridClienteN(idCl);
        }

        public Cliente buscarid(double id)//S
        {
            return new DaoAdministrador().buscaridClienteC(id);
        }

        public void sancionar(Cliente cliente)//S
        {
            new DaoAdministrador().sancionCliente(cliente);
        }

        public Cliente buscaremail(int idCliente)//S
        {
            return new DaoAdministrador().buscarEmailCl(idCliente);
        }

        public void SesionCliente(Cliente cliente)//S
        {
            new DaoAdministrador().sesionCliente(cliente);
        }
    }
}
