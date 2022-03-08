using System;
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
    [RoutePrefix("api/conductor")]
    public class ConductorController : ApiController
    {
        /// <summary>
        /// Ingreso de login conductor
        /// Se solicita la variable Usuario y Contrasena
        /// y se redirecciona a la vista principal del conductor
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("loginConductor")]
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
            Conductor conductor = await new LConductor().login(login);
            if (conductor == null)
            {
                string mensaje = "Usuario y/ o contraseña incorrecta";
                return BadRequest(mensaje);
            }
            else
            {
                var token = TokenGeneratorCo.GenerateTokenJwt(conductor);
                LCerrarSesionConductor conexion = new LCerrarSesionConductor();
                AccesoConductor acceso = new AccesoConductor();
                acceso.FechaInicio = DateTime.Now;
                acceso.Ip = conexion.ip();
                acceso.Mac = conexion.mac();
                acceso.Session = token;
                acceso.IdConductor = conductor.IdConductor;
                await new LCerrarSesionConductor().insertarAcceso(acceso);

                return Ok(token);
            }
        }

        /// <summary>
        /// Hace comentario de la carrera a cliente
        /// El parametro que se recibe es idNotificacion
        /// </summary>
        /// <param name="comentario"></param>
        /// <param name="idNotificacion"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("comentarCarrera")]
        public async Task<IHttpActionResult> putComentarCarrera([FromBody] JObject comentario, int idNotificacion)
        {
            Notificacion notificacion = new Notificacion();
            notificacion.Id = idNotificacion;
            notificacion.ComentarioDeConductor = comentario["ComentarioDeConductor"].ToString();

            await new LODSConductor().comentar(notificacion);
            return Ok("Tu comentario a sido asignado");
        }

        /// <summary>
        /// Se realiza la conversación con el cliente
        /// El parametro que recibe es idNotificacion y usuario del conductor
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
            Conductor conductor1 = new LConductor().mostrarDatosLogin(usuario);
            string nombre = conductor1.Nombre;

            Notificacion notificacion = new Notificacion();
            notificacion.Id = idNotificacion;
            notificacion.Conversacion = "Conductor " + nombre + ": " + chat["Conversacion"].ToString();

            await new LHistorialCarreras().conversar(notificacion);
            return Ok("Enviado");
        }

        /// <summary>
        /// Se acepta la solcitud del servicio
        /// Los parametros que recibe son idNotificacion, idCliente y idConductor (Sesion de conductor logueado)
        /// </summary>
        /// <param name="idNotificacion"></param>
        /// <param name="idCliente"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("aceptar")]
        public async Task<IHttpActionResult> aceptarServicio(int idNotificacion, int idCliente, string usuario)
        {
            LRecuperarCliente correo = new LRecuperarCliente();
            Cliente cliente = new LHistorialCarreras().buscarEmail(idCliente);

            String mensaje = "Su Servicio a sido confirmado por favor espere unos minutos, ya uno de nuestros conductores se acerca al lugar que se solicito";
            correo.enviarCorreoNotificacion(cliente.Email, mensaje);
            
            Notificacion notificacion = new LHistorialCarreras().buscarId(idNotificacion);

            notificacion.Estado = "Aceptado";

            Conductor conductor1 = new LServicioConductor().traerConductor(usuario);
            int idConductor = conductor1.IdConductor;

            notificacion.IdConductor = idConductor;
            Conductor conductor=new LServicioConductor().mostrarDatos(idConductor);
            notificacion.Conductor = conductor.Nombre;

            await new LServicioConductor().aceptar(notificacion);

            return Ok("Servicio Aceptado");
        }

        /// <summary>
        /// Se actualiza el estado del conductor
        /// El parametro que recibe por url es usuario (Session de conductor logueado)
        /// Y por body recibe IdEstado
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("actualizarEstado")]
        public async Task<IHttpActionResult> putEstadoConductor([FromBody] JObject estado, string usuario)
        {
            Conductor conductor1 = new LConductor().mostrarDatosLogin(usuario);
            int idConductor = conductor1.IdConductor;

            Conductor conductor = new Conductor();
            conductor.IdConductor = idConductor;
            conductor.IdEstado = int.Parse(estado["IdEstado"].ToString());

            await new LServicioConductor().estadoConductor(conductor);
            return Ok("Estado actualizado");
        }

        /// <summary>
        /// Muestra la ganancia del conductor 
        /// Recibe como parametro usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("gananciaConductor")]
        public IHttpActionResult  getGanancia(string usuario)
        {
            Conductor conductor1 = new LConductor().mostrarDatosLogin(usuario);
            int idConductor = conductor1.IdConductor;

            try
            {
                Notificacion notificacion = new Notificacion();

                notificacion.IdConductor = idConductor;

                double suma = new LServicioConductor().ganancia(notificacion);
                double ganancia = suma * 0.25;
                Cascaron cascaron = new Cascaron();
                cascaron.Mensaje = ganancia.ToString();

                return Ok("Su ganancia es:"+cascaron.Mensaje);

            }
            catch (Exception ex)
            {
                Cascaron cascaron = new Cascaron();
                cascaron.Mensaje="No tiene ganancias";
                return BadRequest(cascaron.Mensaje);

            }
        }
    }
}
