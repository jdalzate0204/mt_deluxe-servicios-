using Utilitarios;
using Data;
using System.Threading.Tasks;

namespace Logica
{
    public class LConductor
    {
        public async Task<Conductor> login(LoginRequest login) //S
        {
            return await new DaoConductor().login(login);
        }

        public async Task guardarToken(ConductorTokenLogin token) //S
        {
            await new DaoSeguridadConductor().almacenarTokenLogin(token);
        }

        //Para Linea de Profundización 1
        public Conductor mostrarDatosLogin(string usuario) //S
        {
            Conductor conductor = new Conductor();
            conductor.Usuario = usuario;
            return new DaoConductor().mostrarDatosLogin(usuario);
        }
    }
}
