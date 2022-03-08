using Utilitarios;
using Data;
using System.Threading.Tasks;

namespace Logica
{
    public class LAdministrador
    {
        public async Task<Admin> login(LoginRequest login) //S
        {
            return await new DaoAdministrador().login(login);
        }

        public async Task guardarToken(AdministradorTokenLogin token) //S
        {
            await new DaoSeguridadAdmin().almacenarTokenLogin(token);
        }

        public async Task eliminarToken(AdministradorTokenLogin token) //S
        {
            await new DaoAdministrador().eliminarToken(token);
        }

        //Para Linea de Profundización 1
        public Admin mostrarDatosLogin(string usuario) //S
        {
            Admin administrador = new Admin();
            administrador.Usuario = usuario;
            return new DaoAdministrador().mostrarDatosLogin(usuario);
        }

        public void registro(Admin admin)
        {
            new DaoAdministrador().inserAdmin(admin);
        }

    }
}
