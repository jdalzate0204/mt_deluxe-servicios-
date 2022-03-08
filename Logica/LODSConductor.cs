using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Utilitarios;

namespace Logica
{
    public class LODSConductor
    {
        public List<Estado> estado() //S
        {
            return new DaoConductor().estado();
        }

        public async Task<List<Notificacion>> mostrarHistorial(int idEstado) //S
        {
            return await new DaoConductor().mostrarHistorial(idEstado);
        }

        public async Task<List<Notificacion>> filtrarCarrera(DateTime? fechaInicio, int idConductor) //S
        {
            Conductor conductor = new Conductor();
            conductor.IdConductor = idConductor;
            return await new DaoConductor().filtrarCarrera(fechaInicio, idConductor);
        }

        public async Task comentar(Notificacion notificacion) //S
        {
            await new DaoConductor().comentar(notificacion);
        }
    }
}
