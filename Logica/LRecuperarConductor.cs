using System;
using System.Net.Mail;
using System.Text;
using Utilitarios;
using Data;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Logica
{
    public class LRecuperarConductor
    {
        public void enviarCorreo(String correoDestino, String userToken, String mensaje)
        {
            try
            {
                //Configuración del Mensaje
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress("mototaxideluxe@gmail.com", "Correo de Recuperación");

                //Aquí ponemos el asunto del correo
                mail.Subject = "Recuperación Contraseña";
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

        public void generarToken(string usuario)
        {
            Conductor conductor = new DaoConductor().getloginByUsuariocon(usuario);
            TokenConductor token = new TokenConductor();
            token.Creado = DateTime.Now;
            token.IdConductor = conductor.IdConductor;
            token.Vigencia = DateTime.Now.AddMinutes(60);

            token.Token = encriptar(JsonConvert.SerializeObject(token));

            new DaoSeguridadConductor().insertarToken(token);

            String mensaje = "Su token de recuperacion es:" + token.Token + "\nRecuerde que tiene 1 hora de vigencia para realizar al acción";
            enviarCorreo(conductor.Email, token.Token, mensaje);

            Cascaron cascaron = new Cascaron();
            cascaron.Mensaje = "Su nueva contraseña ha sido enviada a su correo";
        }

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

        public async Task recuperar(Conductor conductor)
        {
            
          await   new DaoSeguridadConductor().updateClave(conductor);
       
        }
        
       
  
        public Conductor validar(Conductor conductor)
        {
            return new DaoSeguridadConductor().validarExistencia(conductor);
        }

        public TokenConductor getTokenByUser(int idConductor)
        {
            return new DaoSeguridadConductor().getTokenByUser(idConductor);
        }


        public TokenConductor validarToken(string token)
        {
            return new DaoSeguridadConductor().getTokenByToken(token);
        }
    }
}
