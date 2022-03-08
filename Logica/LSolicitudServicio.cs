using Utilitarios;
using Data;
using System.Threading.Tasks;
using Utilitarios.Entrada;
using System;

namespace Logica
{
    public class LSolicitudServicio
    {
        public async Task servicio(ServicioClienteRequest notificacion1, int idCliente) //S
        {
          
            await new DaoCliente().inserServicio(notificacion1, idCliente);
        }

        public Notificacion mostrarNotificacion(int idCliente)
        {
       
           return new DaoCliente().mostrarNotificacion(idCliente);
        }

        public Cliente mostrarDatos(string usuario)
        {
            return new DaoCliente().mostrarDatos(usuario);
        }

        public Cascaron tarifa(Cascaron cascaron) //S
        {
            double tarifa;
            int destino = cascaron.Destino;
            int ubicacion = cascaron.Ubicacion;


            //Centro -> Pilaca && Pilaca -> Centro
            if ((destino == 10 && ubicacion == 1) || (destino == 1 && ubicacion == 10))
            {
                cascaron.Kilometros = "9,5";
                tarifa = 9.5 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Centro -> Limonal && Limonal -> Centro
            else if ((destino == 10 && ubicacion == 2) || (destino == 2 && ubicacion == 10))
            {
                cascaron.Kilometros = "6,9";
                tarifa = 6.9 * 600;
                cascaron.Pago = tarifa.ToString();
            }

            //Centro -> La granja && La granja -> Centro
            else if ((destino == 10 && ubicacion == 3) || (destino == 3 && ubicacion == 10))
            {
                cascaron.Kilometros = "7,7";
                tarifa = 7.7 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Centro -> Mesetas && Mesetas -> Centro
            else if ((destino == 10 && ubicacion == 4) || (destino == 4 && ubicacion == 10))
            {
                cascaron.Kilometros = "1,2";
                tarifa = 1.2 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Centro -> Santa Ana && Santa Ana -> Centro
            else if ((destino == 10 && ubicacion == 5) || (destino == 5 && ubicacion == 10))
            {
                cascaron.Kilometros = "9,7";
                tarifa = 9.7 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Centro -> La mercedes && La merceedes -> Centro
            else if ((destino == 10 && ubicacion == 6) || (destino == 6 && ubicacion == 10))
            {
                cascaron.Kilometros = "6,4";
                tarifa = 6.4 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Centro ->San bernardo && San bernardo -> Centro
            else if ((destino == 10 && ubicacion == 8) || (destino == 8 && ubicacion == 10))
            {
                cascaron.Kilometros = "12";
                tarifa = 12 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Centro ->Guane && Guane -> Centro
            else if ((destino == 10 && ubicacion == 9) || (destino == 9 && ubicacion == 10))
            {
                cascaron.Kilometros = "14";
                tarifa = 14 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Pilaca ->Limonal && Limonal -> Pilaca
            else if ((destino == 1 && ubicacion == 2) || (destino == 2 && ubicacion == 1))
            {
                cascaron.Kilometros = "4,3";
                tarifa = 4.3 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Pilaca ->La granga && La granja -> Pilaca
            else if ((destino == 1 && ubicacion == 3) || (destino == 3 && ubicacion == 1))
            {
                cascaron.Kilometros = "2,3";
                tarifa = 2.3 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Pilaca ->Mesetas && Mesetas -> Pilaca
            else if ((destino == 1 && ubicacion == 4) || (destino == 4 && ubicacion == 1))
            {
                cascaron.Kilometros = "7,9";
                tarifa = 7.9 * 600;
                cascaron.Pago = tarifa.ToString();

            }
            //Pilaca ->Santa Ana && Santa Ana -> Pilaca
            else if ((destino == 1 && ubicacion == 5) || (destino == 5 && ubicacion == 1))
            {
                cascaron.Kilometros = "15";
                tarifa = 15 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Pilaca ->La mercedes && La mercesdes -> Pilaca
            else if ((destino == 1 && ubicacion == 6) || (destino == 6 && ubicacion == 1))
            {
                cascaron.Kilometros = "11";
                tarifa = 11 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Pilaca ->San bernardo && San bernardo -> Pilaca
            else if ((destino == 1 && ubicacion == 8) || (destino == 8 && ubicacion == 1))
            {
                cascaron.Kilometros = "17";
                tarifa = 17 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Pilaca ->Guane && Guane -> Pilaca
            else if ((destino == 1 && ubicacion == 9) || (destino == 9 && ubicacion == 1))
            {
                cascaron.Kilometros = "19";
                tarifa = 19 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Limonal ->La granja && La granja -> Limonal
            else if ((destino == 2 && ubicacion == 3) || (destino == 3 && ubicacion == 2))
            {
                cascaron.Kilometros = "3,1";
                tarifa = 3.1 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Limonal ->Mesetas && Mesetas -> Limonal
            else if ((destino == 2 && ubicacion == 4) || (destino == 4 && ubicacion == 2))
            {
                cascaron.Kilometros = "6,9";
                tarifa = 6.9 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Limonal ->Santa Ana && Santa Ana -> Limonal
            else if ((destino == 2 && ubicacion == 5) || (destino == 5 && ubicacion == 2))
            {
                cascaron.Kilometros = "12";
                tarifa = 12 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Limonal ->Las mercedes && Las mercesdes -> Limonal
            else if ((destino == 2 && ubicacion == 6) || (destino == 6 && ubicacion == 2))
            {
                cascaron.Kilometros = "9,3";
                tarifa = 9.3 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Limonal ->San bernardo && San bernardo -> Limonal
            else if ((destino == 2 && ubicacion == 8) || (destino == 8 && ubicacion == 2))
            {
                cascaron.Kilometros = "14";
                tarifa = 14 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Limonal ->Guane && Guane -> Limonal
            else if ((destino == 2 && ubicacion == 9) || (destino == 9 && ubicacion == 2))
            {
                cascaron.Kilometros = "17";
                tarifa = 17 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //La granja ->Mesetas && Mesetas -> La granja
            else if ((destino == 3 && ubicacion == 4) || (destino == 4 && ubicacion == 3))
            {
                cascaron.Kilometros = "5,7";
                tarifa = 5.7 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //La granja ->Santa Ana && Santa Ana -> La granja
            else if ((destino == 3 && ubicacion == 5) || (destino == 5 && ubicacion == 3))
            {
                cascaron.Kilometros = "13";
                tarifa = 13 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //La granja ->La mercedes && La mercedes -> La granja
            else if ((destino == 3 && ubicacion == 6) || (destino == 6 && ubicacion == 3))
            {
                cascaron.Kilometros = "10";
                tarifa = 10 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //La granja ->San bernardo && San bernardo -> La granja
            else if ((destino == 3 && ubicacion == 8) || (destino == 8 && ubicacion == 3))
            {
                cascaron.Kilometros = "15";
                tarifa = 15 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //La granja ->Guane && Guane -> La granja
            else if ((destino == 3 && ubicacion == 9) || (destino == 9 && ubicacion == 3))
            {
                cascaron.Kilometros = "18";
                tarifa = 18 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Mesetas ->Santa Ana && Santa Ana -> Mesetas
            else if ((destino == 4 && ubicacion == 5) || (destino == 5 && ubicacion == 4))
            {
                cascaron.Kilometros = "8,4";
                tarifa = 8.4 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Mesetas ->La mercedes && La mercedes -> Mesetas
            else if ((destino == 4 && ubicacion == 6) || (destino == 6 && ubicacion == 4))
            {
                cascaron.Kilometros = "5,2";
                tarifa = 5.2 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Mesetas ->San bernardo && San bernardo -> Mesetas
            else if ((destino == 4 && ubicacion == 8) || (destino == 8 && ubicacion == 4))
            {
                cascaron.Kilometros = "10";
                tarifa = 10 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Mesetas ->Guane && Guane -> Mesetas
            else if ((destino == 4 && ubicacion == 9) || (destino == 9 && ubicacion == 4))
            {
                cascaron.Kilometros = "13";
                tarifa = 13 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Santa Ana ->La mercedes && La mercedes -> Santa Ana
            else if ((destino == 5 && ubicacion == 6) || (destino == 6 && ubicacion == 5))
            {
                cascaron.Kilometros = "5";
                tarifa = 5 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Santa Ana ->San bernardo && San bernardo -> Santa Ana
            else if ((destino == 5 && ubicacion == 8) || (destino == 8 && ubicacion == 5))
            {
                cascaron.Kilometros = "4,6";
                tarifa = 4.6 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //Santa Ana ->Guane && Guane -> Santa Ana
            else if ((destino == 5 && ubicacion == 9) || (destino == 9 && ubicacion == 5))
            {
                cascaron.Kilometros = "12";
                tarifa = 12 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //La mercedes -> San bernardo && San bernardo -> La mercedes
            else if ((destino == 6 && ubicacion == 8) || (destino == 8 && ubicacion == 6))
            {
                cascaron.Kilometros = "3,1";
                tarifa = 3.1 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //La mercedes -> Guane && Guane -> La mercedes
            else if ((destino == 6 && ubicacion == 9) || (destino == 9 && ubicacion == 6))
            {
                cascaron.Kilometros = "7,7";
                tarifa = 7.7 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            //San bernardo -> Guane && San bernardo -> Guane
            else if ((destino == 8 && ubicacion == 9) || (destino == 9 && ubicacion == 8))
            {
                cascaron.Kilometros = "8,3";
                tarifa = 8.3 * 600;
                cascaron.Pago = tarifa.ToString();
            }
            return cascaron;
        }

        public Notificacion factura(Notificacion notificacion, int idCliente) //S
        {
            Notificacion factura = new DaoCliente().generarFactura(notificacion, idCliente);
            return factura;
        }
    }
}
