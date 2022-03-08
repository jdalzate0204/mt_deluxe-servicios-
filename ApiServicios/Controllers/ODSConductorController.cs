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
    [RoutePrefix("api/conductor")]
    public class ODSConductorController : ApiController
    {
        /// <summary>
        /// Muestra lista de estado conductor
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("estado")]
        public List<Estado> getEstado()
        {
            return new LODSConductor().estado();
        }

        /// <summary>
        /// Muestra el historial de carreras del conductor
        /// Se solicitan las variables fechaInicio (bbdd: fechaCarrera) y idConductor
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("historial")]
        public async Task<List<Notificacion>> filtrarCarrera(DateTime? fechaInicio, string usuario)
        {
            Conductor conductor = new LConductor().mostrarDatosLogin(usuario);
            int idConductor=conductor.IdConductor;
            return await new LODSConductor().filtrarCarrera(fechaInicio, idConductor);
        }

        /// <summary>
        /// Muestra el listado de solicitudes de servicios a aceptar
        /// Recibe el parametro de usuario (Sesion de usuario logueado)
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("servicios")]
        public async Task<List<Notificacion>> mostrarServicios(string usuario)
        {
            Conductor conductor1 = new LConductor().mostrarDatosLogin(usuario);
            int idEstado = conductor1.IdEstado;

            return await new LODSConductor().mostrarHistorial(idEstado);
        }
    }
}
