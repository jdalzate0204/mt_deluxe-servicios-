using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Utilitarios;
using Logica;
using Newtonsoft.Json.Linq;
using System;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/conductor")]
    public class RecuperarContraseñaConductorController : ApiController
    {
        /// <summary>
        /// se genera el token para poder recuperar la contraseña
        /// se recibe el parametro de usuario
        /// </summary>
        /// <param name="conductor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("generarContrasena")]
        [AllowAnonymous]
        public IHttpActionResult generar(Conductor conductor)
        {
            try
            {
                Conductor conductorE = new Conductor();
                Cascaron cascaron = new Cascaron();
                TokenConductor token = new TokenConductor();
                conductorE = new LRecuperarConductor().validar(conductor);

                if (conductorE != null)
                {
                    if (new LRecuperarConductor().getTokenByUser(conductor.IdConductor) == null)
                    {
                        new LRecuperarConductor().generarToken(conductor.Usuario);
                        cascaron.Tokenco = token;
                        cascaron.Mensaje = "Recibira un correo con el link para continuar con el proceso";
                    }
                    else if (token.Vigencia < DateTime.Now)
                    {
                        return BadRequest("Token Vencido");
                    }
                }
                else
                {
                    return BadRequest("El usuario no exite, se encuentra sancionado o no se encuentra aceptado por el momento, por favor verifique");
                }

                return Ok(cascaron.Mensaje);

            }catch (Exception ex)
            {
                return BadRequest("error");
            }
        }

        /// <summary>
        ///  Recupera la contraseña
        /// Recibe como parametro por url el token y por Body Contrasena y ContrasenaConfirmada
        /// </summary>
        /// <param name="contrasena"></param>
        /// <param name="tokenRecibido"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("RecuperarContrasena")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> recuperar([FromBody]JObject contrasena, string tokenRecibido)
        {
            try
            {
                Conductor conductor = new Conductor();
                TokenConductor token = new TokenConductor();
                conductor.Contrasena = contrasena["Contrasena"].ToString();
                string contrasena2 = contrasena["ContrasenaConfirmada"].ToString();
                if (conductor.Contrasena.Equals(contrasena2))
                {
                    token = new LRecuperarConductor().validarToken(tokenRecibido);
                    conductor.IdConductor = token.IdConductor;

                    await new LRecuperarConductor().recuperar(conductor);
                    return Ok("'Su contraseña fue actualizada");
                }
                else
                {
                    return Ok("Las contraseñas no coinciden");
                }
            }
            catch(Exception ex)
            {
                return BadRequest("token no valido o algun dato esta mal escrito");
            }
           
        }
    }

}
