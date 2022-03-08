using Utilitarios;
using Data;
using Utilitarios.Entrada;
using System.Threading.Tasks;

namespace Logica
{
    public class LRegistroConductor
    {
        public async Task registro(RegistroConductorRequest registro) //S
        {
            await new DaoConductor().inserConductor(registro);
        }

        public Conductor validarUsuario(RegistroConductorRequest conductor) //S
        {
            return new DaoConductor().validarExistencia(conductor);
        }

        public Conductor validarCorreo(RegistroConductorRequest conductor) //S
        {
            return new DaoConductor().validarExistenciaCorreo(conductor);
        }

    }

}
