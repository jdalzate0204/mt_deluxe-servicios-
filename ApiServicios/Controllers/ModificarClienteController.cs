using System;
using System.Web.Http;
using Utilitarios;
using Logica;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Web.Http.Cors;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cliente")]
    public class ModificarClienteController : ApiController
    { 
        /// <summary>
        /// Muestra la informacion del cliente 
        /// el parametro que recibe es el usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("informacion")]
        public async Task<IHttpActionResult> getMostrarDatosLogin(string usuario)
        {
            try
            {
                Cliente cliente1 = new LCliente().mostrarDatosLogin(usuario);
                int idCliente;
                idCliente = cliente1.IdCliente;

                Cliente cliente = await new LModificarCliente().mostrar(idCliente);

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest("el usuario no existe");
            }
        }

        /// <summary>
        /// Se realiza la actualizacion de algun  registro cliente
        /// Pueden ingresar las variables: Nombrecl, Apellido, FechaDeNacimiento, Email, Usuario, Contrasena
        /// </summary>
        /// <param name="clienteInformacion"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("actualizarDatos")]
        public async Task<IHttpActionResult> putActulizarDatos([FromBody] JObject clienteInformacion,string usuario)
        {
            try
            {
                Cliente cliente1 = new LCliente().mostrarDatosLogin(usuario);
                int idCliente;
                idCliente = cliente1.IdCliente;

                Cliente cliente = new Cliente();

                cliente.IdCliente = idCliente;
                cliente.Nombrecl = clienteInformacion["Nombrecl"].ToString();
                cliente.Apellido = clienteInformacion["Apellido"].ToString();
                cliente.FechaDeNacimiento = DateTime.Parse(clienteInformacion["FechaDeNacimiento"].ToString());
                cliente.Email = clienteInformacion["Email"].ToString();
                cliente.Usuario = clienteInformacion["Usuario"].ToString();
                cliente.Contrasena = clienteInformacion["Contrasena"].ToString();
                cliente.Modificado = clienteInformacion["Usuario"].ToString();

                Cascaron cascaron = await new LModificarCliente().modificar(cliente);

                return Ok("Sus datos han sido actualizados");

            }catch(Exception ex)
            {
                return BadRequest("revise las entradas");
            }

        }

    }
}
