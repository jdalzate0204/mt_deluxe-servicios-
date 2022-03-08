using System.Threading.Tasks;
using System.Web.Http;
using Utilitarios;
using Logica;
using System.Web.Http.Cors;
using System;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/conductor")]
    public class EliminarConductorController : ApiController
    {
        /// <summary>
        /// Elimina la cuenta 
        /// recibe como parametro el usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("eliminarCuenta")]
        public async Task<IHttpActionResult> putActulizarSesion(string usuario)
        {
            try
            {
                Conductor conductor1 = new LConductor().mostrarDatosLogin(usuario);
                int idConductor = conductor1.IdConductor;

                Conductor conductor = new Conductor();
                conductor.IdConductor = idConductor;

                Cascaron cascaron = await new LEliminarConductor().eliminar(conductor);

                return Ok("tu cuenta a sido eliminada");
            }catch(Exception ex)
            {
                return BadRequest("el usuario no existe");
            }
        }

        /// <summary>
        /// Elimina el token (se usa junto a cerrar sesion y eliminar cuenta)
        /// Recibe como parametro el usuario (sesion logueada)
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("eliminarToken")]
        public async Task<IHttpActionResult> delEliminarToken(string usuario)
        {
            try
            {
                Conductor conductor1 = new LConductor().mostrarDatosLogin(usuario);
                int idConductor = conductor1.IdConductor;

                ConductorTokenLogin token = new ConductorTokenLogin();
                token.IdConductor = idConductor;
                await new LEliminarConductor().eliminarToken(token);
                return Ok("El token fue eliminado");

            }catch(Exception ex)
            {
                return BadRequest("el usuario no existe");
            }
        }
    }
}
