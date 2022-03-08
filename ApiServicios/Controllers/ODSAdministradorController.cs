using System.Collections.Generic;
using System.Web.Http;
using Utilitarios;
using Logica;
using System.Web.Http.Cors;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/administrador")]
    public class ODSAdministradorController : ApiController
    {
        /// <summary>
        /// Muestra el listado de conductores registrados
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("mostrarConductores")]
        public List<Conductor> mostrarConductores()
        {
            return new LODSAdministrador().mostrarConductores();
        }

        /// <summary>
        /// Muestra el listado de clientes registrados
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        [Route("mostrarClientes")]
        public List<Cliente> mostrarClientes()
        {
            return new LODSAdministrador().mostrarClientes();
        }

        /// <summary>
        /// Muestra listado de conductores registrados y sancionados para ponerlos activos 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("mostrarConductoresAceptar")]
        public List<Conductor> mostrarConductoresAceptar()
        {
            return new LODSAdministrador().mostrarConductorAceptar();
        }

        /// <summary>
        /// Muestra listado de clientes aceptar registros sancionados para ponerlos activos 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("mostrarClientesAceptar")]
        public List<Cliente> mostrarClienteAceptar()
        {
            return new LODSAdministrador().mostrarClienteAceptar();
        }

        /// <summary>
        /// Muestra listado de conductores para pago
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("mostrarConductoresPago")]
        public List<Notificacion> mostrarConductoresPago()
        {
            return new LODSAdministrador().conductoresPago();
        }

        /// <summary>
        /// Muestra el listado de carreras de conductor para poder sancionar 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("mostrarCarreraConductor")]
        public List<Notificacion> mostrarCarreraConductor()
        {
            return new LODSAdministrador().mostrarCarreraConductor();
        }


        /// <summary>
        /// Muestra el listado de servicios de cliente para poder sancionar 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("mostrarServicioCliente")]
        public List<Notificacion> mostrarServicioCliente()
        {
            return new LODSAdministrador().mostrarServiciosCliente();
        }
    }
}
