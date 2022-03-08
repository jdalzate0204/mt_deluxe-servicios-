using System.Threading.Tasks;
using System.Web.Http;
using Utilitarios;
using Logica;
using System.Web.Http.Cors;
using System;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cliente")]
    public class EliminarClienteController : ApiController
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
                Cliente cliente1 = new LCliente().mostrarDatosLogin(usuario);
                int idCliente;
                idCliente = cliente1.IdCliente;

                Cliente cliente = new Cliente();
                cliente.IdCliente = idCliente;

                Cascaron cascaron = await new LEliminarCliente().eliminar(cliente);

                return Ok("tu cuenta a sido eliminada");
            }catch (Exception ex)
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
            try { 
               Cliente cliente = new LCliente().mostrarDatosLogin(usuario);
               int idCliente = cliente.IdCliente;
               ClienteTokenLogin token = new ClienteTokenLogin();
               token.IdCliente = idCliente;
               await new LEliminarCliente().eliminarToken(token);
               return Ok("El token fue eliminado");

            }catch (Exception ex)
            {
                return BadRequest("el usuario no existe");
            }
        }
    }
}
