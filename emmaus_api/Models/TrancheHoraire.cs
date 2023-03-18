using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("tranche_horaire")]
    public class TrancheHoraire
    {
        [Key]
        public int Id { get; set; }

        [Column("heure_debut")]
        [Required]
        public TimeSpan HeureDebut { get; set; }

        [Column("heure_fin")]
        [Required]
        public TimeSpan HeureFin { get; set; }

        // relation avec la table Ramasse
        public ICollection<Ramasse> Ramasses { get; set; }

        // relation avec la table Livraison
        public ICollection<Livraison> Livraisons { get; set; }
    }

}
