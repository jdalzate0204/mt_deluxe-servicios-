using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Utilitarios;
using Utilitarios.Entrada;

namespace Data
{
    public class DaoSeguridadCliente
    {
        //Guarda token de login (S)
        public async Task almacenarTokenLogin(ClienteTokenLogin token)
        {
            using (var db = new MapeoCliente()) 
            {
                db.tokenLogin.Add(token);
                await db.SaveChangesAsync();
            }
        }

        //Captura el token ingresado (S)
        public ClienteTokenLogin getTokenLogin(string token)
        {
            using(var db = new MapeoCliente())
            {
                return db.tokenLogin.Where(x => x.Token.Equals(token)).FirstOrDefault();
            }
        }

        //Generar token recuperar(S)
        public void  insertarToken(TokenCliente token)
        {
            using (var db = new MapeoCliente())
            {
                db.token.Add(token);
                db.SaveChanges();
            }
        }

        //Validar existencia de usuario
        public Cliente validarExistencia(Cliente clienteE)
        {
           
            return new MapeoCliente().client.Where(x => x.Usuario.Equals(clienteE.Usuario) && x.Sesion.Equals("activo")).FirstOrDefault();
        }

        //Inserta acceso al iniciar sesion
        public async Task insertarAcceso(AccesoCliente accesoCliente)
        {
            using (var db = new MapeoCliente())
            {
                db.accesoClientes.Add(accesoCliente);
                await db.SaveChangesAsync();
            }
        }

        //Cierra acceso al cerrar sesion
        public async Task cerrarAcceso(int id_cliente)
        {
            using (var db = new MapeoCliente())
            {
                AccesoCliente acceso = db.accesoClientes.Where(x => x.IdCliente == id_cliente && x.FechaFin == null).FirstOrDefault();
                acceso.FechaFin = DateTime.Now;

                db.accesoClientes.Attach(acceso);

                var entry = db.Entry(acceso);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Valida usuario y vigencia token de cliente(S)
        public TokenCliente getTokenByUser(int usuarioId)
        {
            return new MapeoCliente().token.Where(x => x.IdCliente == usuarioId && x.Vigencia > DateTime.Now).FirstOrDefault();
        }

        //Valida token para recuperar
        public TokenCliente getTokenByToken(string tokenn)
        {
            return new MapeoCliente().token.Where(x => x.Token == tokenn).FirstOrDefault();
        }

        //Modifica contraseña (recuperar)
        public async Task updateClave(Cliente cliente)
        {
            using (var db = new MapeoCliente())
            {
                Cliente usuarioAnterior = db.client.Where(x => x.IdCliente == cliente.IdCliente).First();
                usuarioAnterior.Contrasena = cliente.Contrasena;

                db.client.Attach(usuarioAnterior);

                var entry = db.Entry(usuarioAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
    }
}
