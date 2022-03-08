using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Utilitarios;
using Logica;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/administrador")]
    public class CerrarSesionAdministradorController : ApiController
    {
        /// <summary>
        /// Se elimina el token para cerrar sesion
        /// recibe como parametro el usuario (sesion logueada)
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("cerrarSesion")]
        public async Task<IHttpActionResult> delEliminarToken(string usuario)
        {
            Admin administrador = new LAdministrador().mostrarDatosLogin(usuario);
            int idAdmin;
            idAdmin = administrador.IdAdmin;

            AdministradorTokenLogin token = new AdministradorTokenLogin();
            token.IdAdmin = idAdmin;
            await new LAdministrador().eliminarToken(token);
            return Ok("El token fue eliminado");
        }
    }
}
