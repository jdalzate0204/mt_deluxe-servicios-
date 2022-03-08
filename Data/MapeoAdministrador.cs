using System.Data.Entity;
using Utilitarios;

namespace Data
{
    public class MapeoAdministrador : DbContext
    {
        static MapeoAdministrador()
        {
            Database.SetInitializer<MapeoAdministrador>(null);
        }

        public MapeoAdministrador()
            : base("name=bd_proyecto")
        {

        }

        public DbSet<Admin> administrador { get; set; }
        public DbSet<Notificacion> notificacion { get; set; }
        public DbSet<AdministradorTokenLogin> tokenLogin { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.HasDefaultSchema("public");
            base.OnModelCreating(builder);
        }
    }
}
