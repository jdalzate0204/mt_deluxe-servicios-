using Utilitarios;
using Data;
using System.Threading.Tasks;

namespace Logica
{
    public class LModificarConductor
    {
        public async Task<Conductor> mostrar(int idConductor) //S
        {
            return await new DaoConductor().mostrarRegistro(idConductor);
        }

        public async Task<Cascaron> modificar(Conductor conductor) //S
        {
            Cascaron cascaron = new Cascaron();
            cascaron.Conductor = conductor;
            if (cascaron.Conductor != null)
            {
                await new DaoConductor().modificarConductor(conductor);
                cascaron.Url = "modificarConductor.aspx";
            }
            return cascaron;
        }
    }
}
