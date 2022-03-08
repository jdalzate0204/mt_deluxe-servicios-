using System;
using System.Web.Http;
using Utilitarios;
using Logica;
using ApiServicios.Seguridad;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/administrador")]
    public class AdminController : ApiController
    {
        /// <summary>
        /// Ingreso de login administrador
        /// Se solicita la variable Usuario y Contrasena
        /// y se redirecciona a la vista principal del administrador
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("loginAdministrador")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> login(LoginRequest login)
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
            Admin admin = await new LAdministrador().login(login);
            if (admin == null)
            {
                string mensaje = "Usuario y/o contraseña incorrecta";
                return BadRequest(mensaje);
            }
            else
            {
                var token = TokenGeneratorAd.GenerateTokenJwt(admin);
                return Ok(token);
            }
        }

        /// <summary>
        /// Se acepta solicitud de registro de conductor
        /// Recibe como parametro el idConductor
        /// </summary>
        /// <param name="idConductor">Identificador del conductor</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("aceptarSolicitudConductor")]
        public IHttpActionResult  putAceptarSolicitud(int idConductor)
        {
          
                LAceptarSancionarConductor correo = new LAceptarSancionarConductor();
                Conductor conductor = new LAceptarSancionarConductor().buscarid(idConductor);

                String mensaje = "Su cuenta ya se encuentra activa, puedes ofrecer nuestros servicio a nuestros clientes";
                correo.enviarCorreoNotificacion(conductor.Email, mensaje);
                conductor.Sesion = "activo";

                new LAceptarSancionarConductor().sesionConductor(conductor);
                Cascaron cascaron = new Cascaron();

                cascaron.Mensaje = "Solicitud Aceptada";

                return Ok(cascaron.Mensaje);
            

        }

        /// <summary>
        /// Se acepta el registro cliente para que quede activa
        /// Recibe como parametro el idCliente
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("aceptarCliente")]
        public IHttpActionResult putAceptarCliente(int idCliente)
        {
            
            LAceptarSancionarConductor correo = new LAceptarSancionarConductor();
            Cliente cliente = new LSancionarCliente().buscaremail(idCliente);

            String mensaje = "Su cuenta ya a sido activa";
            correo.enviarCorreoNotificacion(cliente.Email, mensaje);
            cliente.Sesion = "activo";

            new LSancionarCliente().SesionCliente(cliente);

            Cascaron cascaron = new Cascaron();

            cascaron.Mensaje = "Solicitud Aceptada";

            return Ok(cascaron.Mensaje);
        }

        /// <summary>
        /// Se sanciona el conductor
        /// Recibe como parametro el idConductor
        /// </summary>
        /// <param name="idConductor"></param>
        [Authorize]
        [HttpPut]
        [Route("sancionarConductor")]
        public IHttpActionResult putSancionarConductor(int idConductor)
        {
           
            Notificacion notificacion = new LAceptarSancionarConductor().buscaridco(idConductor);
            Conductor conductor = new LAceptarSancionarConductor().buscarcono(idConductor);

            conductor.FechaSancion = DateTime.Now;
            conductor.Sesion = "sancionado";

            new LAceptarSancionarConductor().sancionarConductor(conductor);

            LAceptarSancionarConductor correo = new LAceptarSancionarConductor();
            string mensaje = "Tu cuenta a sido sancionada por inconformidad de los usuarios . Espera que uno de nuestros administradores vuelva a activar tu cuenta";
            correo.enviarCorreoNotificacion(conductor.Email, mensaje);

            return Ok("revisa tu correo");
        }

        /// <summary>
        /// Se sanciona el cliente
        /// Recibe como parametro del idCliente
        /// </summary>
        /// <param name="idCliente"></param>
        [Authorize]
        [HttpPut]
        [Route("sancionarCliente")]
        public IHttpActionResult putSancionarCliente(int idCliente)
        {
            Notificacion notificacion = new LSancionarCliente().buscaridN(idCliente);
            Cliente cliente = new LSancionarCliente().buscarid(idCliente);

            cliente.FechaSancion = DateTime.Now;
            cliente.Sesion = "sancionado";

            new LSancionarCliente().sancionar(cliente);

            LAceptarSancionarConductor correo = new LAceptarSancionarConductor();
            string mensaje = "Tu cuenta a sido sancionada por inconformidad de los usuarios . Espera que uno de nuestros administradores te vuelva activar tu cuenta";
            correo.enviarCorreoNotificacion(cliente.Email, mensaje);

            return Ok("revisa tu correo");
        }
    }
}

