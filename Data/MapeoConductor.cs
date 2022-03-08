using System.Data.Entity;
using Utilitarios;

namespace Data
{
    public class MapeoConductor:DbContext
    {
        static MapeoConductor()
        {
            Database.SetInitializer<MapeoConductor>(null);
        }

        public MapeoConductor()
            : base("name=bd_proyecto")
        {

        }

        public DbSet<Conductor> conduc { get; set; }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<TokenConductor> token { get; set; }
        public DbSet<AccesoConductor> accesoconductor { get; set; }
        public DbSet<Estado> estado { get; set; }
        public DbSet<Notificacion> notificacion { get; set; }
        public DbSet<ConductorTokenLogin> tokenLogin { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.HasDefaultSchema("public");
            base.OnModelCreating(builder);
        }
    }
}
