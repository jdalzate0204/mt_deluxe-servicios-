using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Data;
using Utilitarios;

namespace Logica
{
    public class LAceptarSancionarConductor
    {
        private string encriptar(string input)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            return output.ToString();
        }

        public void enviarCorreoNotificacion(String correoDestino, String mensaje)
        {
            try
            {
                //Configuración del Mensaje
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress("mototaxideluxe@gmail.com", "Servicio Mototaxi");

                //Aquí ponemos el asunto del correo
                mail.Subject = "Confirmacion de servicio";
                //Aquí ponemos el mensaje que incluirá el correo
                mail.Body = mensaje;
                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                mail.To.Add(correoDestino);

                SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
                                       //Especificamos las credenciales con las que enviaremos el mail
                SmtpServer.Credentials = new System.Net.NetworkCredential("mototaxideluxe@gmail.com", "Deluxe123");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {

            }
        }

        public Conductor buscarid(int idConductor)//s
        {
            return new DaoAdministrador().buscarEmail(idConductor);
        }

        public void sesionConductor(Conductor conductor)
        {
            new DaoAdministrador().sesionConductor(conductor);
        }

        public Notificacion buscaridco(double idco)//S
        {
            return new DaoAdministrador().buscaridConductorN(idco);
        }

        public Conductor buscarcono(double idco)//S
        {
            return new DaoAdministrador().buscaridConductorCo(idco);
        }

        public void sancionarConductor(Conductor conductor)//S
        {
            new DaoAdministrador().sancionConductor(conductor);
        }

        public Notificacion generar(Notificacion notificacion) //S
        {
            
            return new DaoAdministrador().generarDesprendible(notificacion);
        }

        public double ganancia(int idConductor)
        {
            return new DaoAdministrador().ganancias(idConductor);
        }
    }
}
