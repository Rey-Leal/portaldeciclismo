using PortalDeCiclismo.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PortalDeCiclismo.DAL
{
    public class PortalContext : DbContext
    {
        public PortalContext() : base("name=PortalDeCiclismo")
        {
        }

        public DbSet<Acesso> Acessos { get; set; }
        public DbSet<Atleta> Atletas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<EtapaDeTreino> EtapasDeTreino { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Treino> Treinos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}