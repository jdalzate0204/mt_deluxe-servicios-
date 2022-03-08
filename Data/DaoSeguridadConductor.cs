using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Utilitarios;

namespace Data
{
    public class DaoSeguridadConductor
    {
        //Guarda token de login (S)
        public async Task almacenarTokenLogin(ConductorTokenLogin token)
        {
            using (var db = new MapeoConductor())
            {
                db.tokenLogin.Add(token);
                await db.SaveChangesAsync();
            }
        }

        //Generar Token
        public void insertarToken(TokenConductor tokenconductor)
        {
            using (var db = new MapeoConductor())
            {
                db.token.Add(tokenconductor);
                db.SaveChanges();
            }
        }

        //Validar existencia de usuario
        public Conductor validarExistencia(Conductor conductorE)
        {
            return new MapeoConductor().conduc.Where(x => x.Usuario.Equals(conductorE.Usuario) && x.Sesion.Equals("activo")).FirstOrDefault();
        }

        //Inserta acceso al iniciar sesion
        public async Task insertarAcceso(AccesoConductor accesoConductor)
        {
            using (var db = new MapeoConductor())
            {
                db.accesoconductor.Add(accesoConductor);
                await db.SaveChangesAsync();
            }
        }

        //Cierra acceso al cerrar sesion
        public async Task cerrarAcceso(int id_conductor)//S
        {
            using (var db = new MapeoConductor())
            {
                AccesoConductor acceso = db.accesoconductor.Where(x => x.IdConductor == id_conductor && x.FechaFin == null).FirstOrDefault();
                acceso.FechaFin = DateTime.Now;

                db.accesoconductor.Attach(acceso);

                var entry = db.Entry(acceso);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        //Valida usuario y vigencia token de conductor
        public TokenConductor getTokenByUser(int usuarioId)
        {
            return new MapeoConductor().token.Where(x => x.IdConductor == usuarioId && x.Vigencia > DateTime.Now).FirstOrDefault();
        }

        //Valida token para recuperar
        public TokenConductor getTokenByToken(string tokenn)
        {
            return new MapeoConductor().token.Where(x => x.Token == tokenn).FirstOrDefault();
        }

        //Modifica contraseña (recuperar)
        public async Task updateClave(Conductor conductor)
        {
            using (var db = new MapeoConductor())
            {
                Conductor usuarioAnterior = db.conduc.Where(x => x.IdConductor == conductor.IdConductor).First();
                usuarioAnterior.Contrasena = conductor.Contrasena;

                db.conduc.Attach(usuarioAnterior);

                var entry = db.Entry(usuarioAnterior);
                entry.State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
    }
}
