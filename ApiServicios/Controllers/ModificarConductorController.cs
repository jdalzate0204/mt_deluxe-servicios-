using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Utilitarios;
using Logica;
using System.Web.Http.Cors;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/conductor")]
    public class ModificarConductorController : ApiController
    {
        /// <summary>
        /// Muestra la informacion del conductor 
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
                Conductor conductor1 = new LConductor().mostrarDatosLogin(usuario);
                int idConductor = conductor1.IdConductor;

                Conductor conductor=await new LModificarConductor().mostrar(idConductor);
                return Ok(conductor);

            }catch(Exception ex)
            {
                return BadRequest("el usuario no existe");
            }
        }

        /// <summary>
        ///  Se realiza la actualizacion de algun  registro de conductor
        /// Se solicitan las variables: Nombre, Apellido,Cedula,Celular, FechaDeNacimiento, Email, Usuario, Contrasena, Placa
        /// y se redirecciona a login conductor
        /// el parametro que recibe es el usuario
        /// </summary>
        /// <param name="conductorInformacion"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("actualizarDatos")]
        public async Task<IHttpActionResult> putActulizarDatos([FromBody] JObject conductorInformacion, string usuario)
        {
            try
            {
                Conductor conductor1 = new LConductor().mostrarDatosLogin(usuario);
                int idConductor = conductor1.IdConductor;

                Conductor conductor = new Conductor();

                conductor.IdConductor = idConductor;
                conductor.Nombre = conductorInformacion["Nombre"].ToString();
                conductor.Apellido = conductorInformacion["Apellido"].ToString();
                conductor.FechaDeNacimiento = DateTime.Parse(conductorInformacion["FechaDeNacimiento"].ToString());
                conductor.Email = conductorInformacion["Email"].ToString();
                conductor.Placa = conductorInformacion["Placa"].ToString();
                conductor.Celular = conductorInformacion["Celular"].ToString();
                conductor.Usuario = conductorInformacion["Usuario"].ToString();
                conductor.Contrasena = conductorInformacion["Contrasena"].ToString();
                conductor.Modificado = conductorInformacion["Usuario"].ToString();

                Cascaron cascaron = await new LModificarConductor().modificar(conductor);

                return Ok("Sus datos han sido actualizados");

            }catch(Exception ex)
            {
                return BadRequest("revise las entradas");
            }
        }
    }
}
