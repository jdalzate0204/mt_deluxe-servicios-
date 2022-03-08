using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Utilitarios;
using Logica;
using ApiServicios.Seguridad;
using Newtonsoft.Json.Linq;
using System.Web.Http.Cors;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        /// <summary>
        /// Ingreso de login cliente
        /// Se solicita la variable Usuario y Contrasena
        /// y se redirecciona a la vista principal del cliente
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("loginCliente")]
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
            Cliente cliente = await new LCliente().login(login);
            if (cliente == null)
            {
                string mensaje = "Usuario y/o contraseña incorrecta";
                return BadRequest(mensaje);
            }
            else
            {
                var token = TokenGeneratorCl.GenerateTokenJwt(cliente);
                LCerrarSesionCliente conexion = new LCerrarSesionCliente();
                AccesoCliente acceso = new AccesoCliente();
                acceso.FechaInicio = DateTime.Now;
                acceso.Ip = conexion.ip();
                acceso.Mac = conexion.mac();
                acceso.Session = token;
                acceso.IdCliente = cliente.IdCliente;
                await new LCerrarSesionCliente().insertarAcceso(acceso);
               
                return Ok(token);
            }
        }
        
      
        /// <summary>
        /// Hace comentario del servicio a conductor
        /// el parametro quese recibe es idNoticicacion
        /// </summary>
        /// <param name="comentario"></param>
        /// <param name="idNotificacion"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("comentarServicio")]
        public async Task<IHttpActionResult> putComentarServicio([FromBody]JObject comentario, int idNotificacion)
        {
            Notificacion notificacion = new Notificacion();
            notificacion.Id = idNotificacion;
            notificacion.ComentarioDeCliente = comentario["ComentarioDeCliente"].ToString();

            await new LODSCliente().comentar(notificacion);
            return Ok("Tu comentario a sido asigando");
        }

        /// <summary>
        /// Se realiza la conversación con el conductor
        /// El parametro que recibe es idNotificacion y nombre del cliente
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="idNotificacion"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("conversar")]
        public async Task<IHttpActionResult> putConversar([FromBody] JObject chat, int idNotificacion, string usuario)
        {
            Cliente cliente1 = new LCliente().mostrarDatosLogin(usuario);
            string nombre;
            nombre = cliente1.Nombrecl;

            Notificacion notificacion = new Notificacion();
            notificacion.Id = idNotificacion;
            notificacion.Conversacion = "Cliente " +nombre+ ": " + chat["Conversacion"].ToString();
            
            await new LHistorialServicio().conversacion(notificacion);
            return Ok("Envidado");
        }


        /// <summary>
        /// Muestra todos los registros de cliente
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("registros")]
        public List<Cliente> getMostrarregistro()
        {
            return new LCliente().mostrarRegistro();
        }

        /// <summary>
        /// Muestra la informacion del cliente 
        /// el parametro que recibe es el idCliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("informacionRegistro")]
        public async Task<IHttpActionResult> getMostrarDatos(int idCliente)
        {
           Cliente cliente = await new LModificarCliente().mostrar(idCliente);

           return Ok(cliente);

          
        }

        /// <summary>
        /// Se realiza la actualizacion de algun  registro cliente
        /// Pueden ingresar las variables: Nombrecl, Apellido, FechaDeNacimiento, Email, Usuario, Contrasena
        /// </summary>
        /// <param name="clienteInformacion"></param>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("actualizarDatosRegistro")]
        public async Task<IHttpActionResult> putActulizarDatos([FromBody] JObject clienteInformacion, int idCliente)
        {
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
        }

        /// <summary>
        /// Elimina la cuenta 
        /// recibe como parametro el idCliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("eliminarRegistro")]
        public async Task<IHttpActionResult> putActulizarSesion(int idCliente)
        {

            Cliente cliente = new Cliente();
            cliente.IdCliente = idCliente;

             await new LEliminarCliente().eliminarRegistro(cliente);

            return Ok("tu cuenta a sido eliminada");
        }

    }
}
