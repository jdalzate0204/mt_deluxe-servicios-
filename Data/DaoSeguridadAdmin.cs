using System.Threading.Tasks;
using Utilitarios;

namespace Data
{
    public class DaoSeguridadAdmin
    {
        //Guarda token de login (S)
        public async Task almacenarTokenLogin(AdministradorTokenLogin token)
        {
            using (var db = new MapeoAdministrador())
            {
                db.tokenLogin.Add(token);
                await db.SaveChangesAsync();
            }
        }
    }
}
