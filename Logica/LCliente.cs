using Utilitarios;
using Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logica
{
    public class LCliente
    {
        public async Task<Cliente> login(LoginRequest login) //S
        {
            return await new DaoCliente().login(login);
        }

        public async Task guardarToken(ClienteTokenLogin token) //S
        {
            await new DaoSeguridadCliente().almacenarTokenLogin(token);
        }

        public ClienteTokenLogin obtenerConfiguracion(string token)
        {
            return new DaoSeguridadCliente().getTokenLogin(token);
        }
        
        public Cliente mostrarDatosLogin(string usuario) //S
        {
            Cliente cliente = new Cliente();
            cliente.Usuario = usuario;
            return new DaoCliente().mostrarDatosLogin(usuario);
        }

        public List<Cliente> mostrarRegistro() //S
        {
            
            return new DaoCliente().mostrarRegistro();
        }



    }
}
