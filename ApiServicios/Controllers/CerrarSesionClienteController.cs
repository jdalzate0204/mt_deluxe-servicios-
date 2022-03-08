using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Logica;
using Utilitarios;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cliente")]
    public class CerrarSesionClienteController : ApiController
    {
        /// <summary>
        /// Se cierra la sesion del cliente (cierra el acceso)
        /// recibe como parametro el idCliente (sesion logueada)
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("cerrarSesion")]
        public async Task<IHttpActionResult> cerrarSesion(string usuario)
        {
            Cliente cliente = new LCliente().mostrarDatosLogin(usuario);
            int idCliente = cliente.IdCliente;
            await new LCerrarSesionCliente().cerrarSesion(idCliente);
            return Ok("Sesion cerrada");
        }
    }
}
