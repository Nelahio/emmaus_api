using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("fiche_tracabilite")]
    public class FicheTracabilite
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("date_fiche")]
        [Required]
        public DateTime DateFiche { get; set; }

        [Column("id_provenance")]
        [Required]
        public int ProvenanceId { get; set; }

        [ForeignKey("ProvenanceId")]
        public Provenance? Provenance { get; set; }

        public ICollection<TracerFicheMeuble>? TracerFicheMeubles { get; set; }

        public ICollection<FicheTracabiliteCollecte>? FicheTracabiliteCollectes { get; set; }

        public ICollection<FicheTracabiliteReemploi>? FicheTracabiliteReemplois { get; set; }
    }
}
