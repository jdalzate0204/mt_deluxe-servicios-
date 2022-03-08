using Utilitarios;
using Data;
using System.Threading.Tasks;

namespace Logica
{
    public class LHistorialServicio
    {
        public Notificacion buscarIdN(int id)
        {
            Notificacion notificacion = new DaoConductor().buscarId(id);
            return notificacion;
        }

        public async Task<Notificacion> conversacion(Notificacion notificacion) //S
        {
            await new DaoCliente().coversar(notificacion);
            return notificacion;
        }
    }
}
