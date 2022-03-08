using System.Threading.Tasks;
using System.Web.Http;
using Utilitarios;
using Logica;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;
using System;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cliente")]
    public class RecuperarContraseñaClienteController : ApiController
    {
        /// <summary>
        /// se genera el token para poder recuperar la contraseña
        /// se recibe el parametro de usuario
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("generarContrasena")]
        [AllowAnonymous]
        public IHttpActionResult generar(Cliente cliente)
        {
            try
            {
                Cliente clienteE = new Cliente();
                Cascaron cascaron = new Cascaron();
                TokenCliente token = new TokenCliente();
                clienteE = new LRecuperarCliente().validar(cliente);

                if (clienteE != null)
                {
                    if (new LRecuperarCliente().getTokenByUser(clienteE.IdCliente) == null)
                    {
                        cascaron =new LRecuperarCliente().generarToken(cliente.Usuario);
                        cascaron.Mensaje = "Recibira un correo con el link para continuar con el proceso";

                    }
                    else if (token.Vigencia < DateTime.Now)
                    {
                       return BadRequest("Token Vencido");
                    }
                }
                else
                {
                   return  BadRequest("El usuario no exite o está sancionado, por favor verifique");
                }
                return Ok(cascaron);

            }
            catch (Exception ex)
            {
                return BadRequest("error");
            }
        }

        /// <summary>
        /// Recupera la contraseña
        /// Recibe como parametro por url el token y por Body Contrasena y ContrasenaConfirmada
        /// </summary>
        /// <param name="contrasena"></param>
        /// <param name="tokenRecibido"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("RecuperarContrasena")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> recuperar([FromBody]JObject contrasena,string tokenRecibido)
        {
            try
            {
                Cliente cliente = new Cliente();
                TokenCliente token = new TokenCliente();
                cliente.Contrasena = contrasena["Contrasena"].ToString();
                string contrasena2 = contrasena["ContrasenaConfirmada"].ToString();
                if (cliente.Contrasena.Equals(contrasena2))
                {

                    token = new LRecuperarCliente().validarToken(tokenRecibido);
                    cliente.IdCliente = token.IdCliente;

                    await new LRecuperarCliente().recuperar(cliente);
                    return Ok("'Su contraseña fue actualizada");
                }
                else
                {
                    return BadRequest("Las contraseñas no coinciden");
                }
            }catch (Exception ex)
            {
                return BadRequest("token no valido o algun dato esta mal escrito");
            }
            
        }

    }
}
