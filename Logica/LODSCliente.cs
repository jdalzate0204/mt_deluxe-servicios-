using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Utilitarios;

namespace Logica
{
    public class LODSCliente
    {
        public List<Destino> destino() //S
        {
            return new DaoCliente().destino();
        }

        public List<Destino> ubicacion() //S
        {
            return new DaoCliente().ubicacion();
        }

        public List<MPago> pago()  //S
        {
            return new DaoCliente().pago();
        }

        public List<Conductor> conductoresDisponibles() //S
        {
            return new DaoCliente().conductoresDisponibles();
        }

        public async Task<List<Notificacion>> filtrarServicios(DateTime? fechaInicio, int idCliente) //S
        {
            Cliente cliente = new Cliente();
            cliente.IdCliente = idCliente;
            return await new DaoCliente().filtrarServicios(fechaInicio, idCliente);
        }

        public async Task comentar(Notificacion notificacion) //S
        {
            await new DaoCliente().comentar(notificacion);
        }
    }
}
