using Utilitarios;
using Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Logica
{
    public class LServicioConductor
    {
        public async Task estadoConductor(Conductor conductor) //S
        {
            await new DaoConductor().estadoConductor(conductor);
        }

        public async Task aceptar(Notificacion notificacion) //S
        {
            await new DaoConductor().aceptarServicio(notificacion);
        }

        public Conductor mostrarDatos(int idConductor) //S
        {
            return new DaoConductor().mostrarDatos(idConductor);
        }

        public Conductor traerConductor(string usuario)
        {
            return new DaoConductor().traerConductor(usuario);
        }

        public  double ganancia (Notificacion notificacion)//S
        {
           return new DaoConductor().ganancias(notificacion);
        }
    }
}
