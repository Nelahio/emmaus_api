using emmaus_api.Models;
using Microsoft.EntityFrameworkCore;

namespace emmaus_api.Data
{
    public class EmmausContext : DbContext
    {
        public EmmausContext(DbContextOptions<EmmausContext> options) : base(options)
        {
        }
        public DbSet<CategorieMeuble> CategorieMeubles { get; set; }
        public DbSet<Declaration> Declarations { get; set; }
        public DbSet<FicheTracabilite> FicheTracabilites { get; set; }
        public DbSet<FicheTracabiliteCollecte> FicheTracabiliteCollectes { get; set; }
        public DbSet<FicheTracabiliteReemploi> FicheTracabiliteReemplois { get; set; }
        public DbSet<GenererDeclarationFiche> GenererDeclarationFiches { get; set; }
        public DbSet<Livraison> Livraisons { get; set; }
        public DbSet<LivrerMeubleLivraison> LivrerMeubleFicheLivraisons { get; set; }
        public DbSet<Meuble> Meubles { get; set; }
        public DbSet<Provenance> Provenances { get; set; }
        public DbSet<Ramasse> Ramasses { get; set; }
        public DbSet<CollecterRamasseMeuble> CollecterRamasseMeubles { get; set; }
        public DbSet<TracerFicheMeuble> TracerFicheMeubles { get; set; }
        public DbSet<TrancheHoraire> TrancheHoraires { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurez les entités ici si nécessaire.
        }
    }
}
