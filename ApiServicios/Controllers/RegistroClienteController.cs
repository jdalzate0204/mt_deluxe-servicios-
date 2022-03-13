using System.Web.Http;
using Utilitarios;
using Logica;
using System.Threading.Tasks;
using Utilitarios.Entrada;
using System.Web.Http.Cors;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cliente")]
    public class RegistroClienteController : ApiController
    {
        /// <summary>
        /// Se realiza el registro de clientes
        /// Se solicitan las variables: Nombrecl, Apellido, FechaDeNacimiento, Email, Usuario, Contrasena
        /// y se redirecciona a login cliente
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("registroCliente")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> registro(RegistroClienteRequest registro)
        {
            if (!ModelState.IsValid)
            {
                string error = "Entradas incorrectas";
                foreach (var state in ModelState)
                {
                    foreach (var item in state.Value.Errors)
                    {
                        error += $" {item.ErrorMessage}";
                    }
                }
                return BadRequest(error);
            }
            Cliente cliente = new Cliente();
            Cliente clienteV = new LRegistroCliente().validarUsuario(registro);
            Cliente clienteC = new LRegistroCliente().validarCorreo(registro);
            if (clienteV != null)
            {
                string mensaje = "Usuario existente, porfavor intente con otro";
                return BadRequest(mensaje);
            }
            else if (clienteC != null)
            {
                string mensaje = "Correo existente, porfavor intente con otro";
                return BadRequest(mensaje);
            }
            else if (registro.Contrasena != registro.Confirmacion)
            {
                string mensaje = "Las contraseñas no coinciden";
                return BadRequest(mensaje);
            }
            else if (clienteV == null && clienteC == null)
            {
                await new LRegistroCliente().registro(registro);
            }
            return Ok("Su usuario ha sido registrado con exito");
        }
    }
}
