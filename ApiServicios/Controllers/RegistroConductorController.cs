using System.Threading.Tasks;
using System.Web.Http;
using Utilitarios.Entrada;
using Utilitarios;
using Logica;
using System.Web.Http.Cors;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/conductor")]
    public class RegistroConductorController : ApiController
    {
        /// <summary>
        /// Se realiza el registro de conductor
        /// Se solicitan las variables: Nombre, Apellido,Cedula,Celular, FechaDeNacimiento, Email, Usuario, Contrasena, Placa
        /// y se redirecciona a login conductor
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("registroConductor")]
        [AllowAnonymous]
        public async Task <IHttpActionResult> registroConductor(RegistroConductorRequest registro)
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
            Conductor conductor = new Conductor();
            Conductor conductor1 = new LRegistroConductor().validarUsuario(registro);
            Conductor conductor2 = new LRegistroConductor().validarCorreo(registro);

            if (conductor1 != null)
            {
                string mensaje = "Usuario existente, porfavor intente con otro";
                return BadRequest(mensaje);
            }
            else if (conductor2 != null)
            {
                string mensaje = "Correo existente, porfavor intente con otro";
                return BadRequest(mensaje);
            }
            else if (conductor1 == null && conductor2 == null)
            {
                await new LRegistroConductor().registro(registro);
            }
            return Ok("Su usuario ha sido registrado con exito");
        }
    }
}
