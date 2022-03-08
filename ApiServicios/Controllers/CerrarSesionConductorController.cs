using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Logica;
using Utilitarios;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/conductor")]
    public class CerrarSesionConductorController : ApiController
    {
        /// <summary>
        /// Se cierra la sesion del conductor (cierra el acceso)
        /// recibe como parametro el usuario (sesion logueada)
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("cerrarSesion")]
        public async Task<IHttpActionResult> cerrarSesion(string usuario )
        {
            Conductor conductor1 = new LConductor().mostrarDatosLogin(usuario);
            int idConductor = conductor1.IdConductor;
            await new LCerrarSesionConductor().cerrarSesion(idConductor);
            return Ok("Sesion cerrada");
        }
    }
}
