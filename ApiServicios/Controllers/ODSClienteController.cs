using System;
using System.Collections.Generic;
using System.Web.Http;
using Utilitarios;
using Logica;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cliente")]
    public class ODSClienteController : ApiController
    {
        /// <summary>
        /// Muestra listado de destino (y ubicacion)
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("destinos")]
        public List<Destino> getDestinos()
        {
            return new LODSCliente().destino();
        }

        /// <summary>
        /// Muestra listado de ubicacion (y destino)
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("ubicaciones")]
        public List<Destino> getUbicaciones()
        {
            return new LODSCliente().ubicacion();
        }

        /// <summary>
        /// Muestra listado de metodos de pago
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("metodoPago")]
        public List<MPago> getPagos()
        {
            return new LODSCliente().pago();
        }

        /// <summary>
        /// Muestra listado de conductores disponibles
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("conductoresDisponibles")]
        public List<Conductor> getConductoresDisponibles()
        {
            return new LODSCliente().conductoresDisponibles();
        }

        /// <summary>
        /// Muestra el historial de servicio del cliente
        /// Se solicitan las variables fechaInicio (bbdd: fechaCarrera) y usuario
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("historial")]
        public async Task<List<Notificacion>> filtrarServicios(DateTime? fechaInicio, string usuario)
        {
            Cliente cliente = new LCliente().mostrarDatosLogin(usuario);
            int idCliente;
            idCliente=cliente.IdCliente;
            return await new LODSCliente().filtrarServicios(fechaInicio, idCliente);
        }
    }
}
