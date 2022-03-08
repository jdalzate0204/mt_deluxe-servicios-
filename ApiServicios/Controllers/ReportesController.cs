using System.Collections.Generic;
using System.Web.Http;
using Utilitarios;
using Logica;
using System.Web.Http.Cors;
using System;

namespace ApiServicios.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/reportes")]
    public class ReportesController : ApiController
    {
        /// <summary>
        /// Genera reporte de pago de conductor
        /// Recibe como parametro el usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("pagoConductor")]
        public IHttpActionResult desprendible(string usuario) //Administrador
        {
            try
            {
                Conductor conductor1 = new LConductor().mostrarDatosLogin(usuario);
                int idCo = conductor1.IdConductor;

                Notificacion notificacion = new Notificacion();
                double suma = new LAceptarSancionarConductor().ganancia(idCo);
                double ganancia = suma * 0.25;
                notificacion.Tarifa = ganancia;
                notificacion.IdConductor = idCo;

                Notificacion notificacion1=new LAceptarSancionarConductor().generar(notificacion);
              

                return Ok(notificacion1);

            }catch(Exception ex)
            {
                return BadRequest("el usuario no existe");
            }
        }

        /// <summary>
        /// Genera historial de servicio conductor
        /// y recibe como parametro el usuario
        /// </summary>
        /// <param name="notificacion"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("carrerasConductor")]
        public IHttpActionResult reporte(Notificacion notificacion, string usuario) //Conductor
        {
            try
            {
                Conductor conductor1 = new LConductor().mostrarDatosLogin(usuario);
                int idCo = conductor1.IdConductor;

                List<Notificacion> notificacion1 = new LHistorialCarreras().reporte(notificacion, idCo);

                return Ok(notificacion1);
            }catch(Exception ex)
            {
                return BadRequest("usuario no existe");
            }
        }

        /// <summary>
        /// Genera factura de carrera al cliente
        /// recibe como parametro el usuario
        /// </summary>
        /// <param name="notificacion"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("facturaCliente")]
        public IHttpActionResult factura(Notificacion notificacion, string usuario) //Cliente
        {
            try
            {
                Cliente cliente1 = new LCliente().mostrarDatosLogin(usuario);
                int idCliente;
                idCliente = cliente1.IdCliente;

                Notificacion notificacion1=new LSolicitudServicio().factura(notificacion, idCliente);

                return Ok(notificacion1);

            }catch(Exception ex)
            {
                return BadRequest("usuario no existe");
            }
        }
    }
}
