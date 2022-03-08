using Utilitarios;
using Data;
using System.Threading.Tasks;

namespace Logica
{
    public class LEliminarConductor
    {
        public async Task<Cascaron> eliminar(Conductor conductor) //S
        {
            Cascaron cascaron = new Cascaron();
            await new DaoConductor().eliminarConductor(conductor);
            cascaron.Conductor = null;
            cascaron.Url = "loginConductor.aspx";
            return cascaron;
        }

        public async Task eliminarToken(ConductorTokenLogin token) //S
        {
            await new DaoConductor().eliminarToken(token);
        }
    }
}
