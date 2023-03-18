using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("fiche_tracabilite_reemploi")]
    public class FicheTracabiliteReemploi
    {
        [Column("id_fiche_tracabilite")]
        [Key]        
        [Required]
        public int IdFicheTracabilite { get; set; }

        [ForeignKey("IdFicheTracabilite")]
        public FicheTracabilite? FicheTracabilite { get; set; }

        public ICollection<Livraison>? Livraisons { get; set; }
    }
}
