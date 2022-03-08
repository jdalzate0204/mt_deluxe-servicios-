using System;
using System.Threading.Tasks;
using System.Web.Http;
using Utilitarios;
using Logica;
using Newtonsoft.Json.Linq;
using Utilitarios.Entrada;
using System.Web.Http.Cors;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cliente")]
    public class SolicitudServicioController : ApiController
    {
        /// <summary>
        /// Se realiza el registro para solicitar un servicio
        /// Se solicitan las variables: idDestino, idUbicacion, descripcionServicio (opcional), pago, tarifa, kilometro
        /// y usuario (el usuario es utilizado para capturar el idCliente (usuario logueado)
        /// </summary>
        /// <param name="servicio"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("solicitudServicio")]
        public async Task<IHttpActionResult> solicitudServicio([FromBody] JObject servicio)
        {
            try
            {
                ServicioClienteRequest servicioS = new ServicioClienteRequest();

                Cliente cliente = new Cliente();
                
                servicioS.idDestino = int.Parse(servicio["idDestino"].ToString());
                servicioS.idUbicacion = int.Parse(servicio["idUbicacion"].ToString());
                servicioS.descripcionServicio = servicio["descripcionServicio"].ToString();
                servicioS.pago = int.Parse(servicio["pago"].ToString());
                servicioS.tarifa = Double.Parse(servicio["tarifa"].ToString());
                servicioS.kilometros = Double.Parse(servicio["kilometros"].ToString());

                cliente.Usuario = servicio["usuario"].ToString();

                Cliente cliente1 = new LSolicitudServicio().mostrarDatos(cliente.Usuario);

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

                await new LSolicitudServicio().servicio(servicioS, cliente1.IdCliente);
                return Ok("Por favor espera a que uno de nuestros conductores acepte tu solictud, Recibirá un correo notificando su servicio");
 
            }catch (Exception ex)
            {
                return BadRequest("no ha ingresado ningun dato ");
            }
        }

        /// <summary>
        /// Se realiza el calculo de la tarifa del servicio
        /// Se solicitan las variables Destino y Ubicacion
        /// </summary>
        /// <param name="cascaron"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("tarifas")]
        public Cascaron getTarifa(Cascaron cascaron)
        { 
         return new LSolicitudServicio().tarifa(cascaron);
           
        }
    }
}
