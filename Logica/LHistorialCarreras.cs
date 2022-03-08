using System.Collections.Generic;
using Utilitarios;
using Data;
using System.Threading.Tasks;

namespace Logica
{
    public class LHistorialCarreras
    {
        public Cliente  buscarEmail(int idCliente) //S
        {
             return new DaoConductor().buscarEmail(idCliente);
        }

        public Notificacion buscarId(int idNotificacion) //S
        {
            return new DaoConductor().buscarId(idNotificacion);
        }

        public List<Notificacion> reporte(Notificacion notificacion, int idCo) //S
        {
            return new DaoConductor().reporteHistorial(notificacion, idCo);
        }

        public async Task conversar(Notificacion notificacion) //S
        {
            await new DaoConductor().coversar(notificacion);
        }
    }
}
