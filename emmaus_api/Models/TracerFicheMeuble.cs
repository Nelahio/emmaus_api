using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("tracer_fiche_meuble")]
    [PrimaryKey(nameof(MeubleId), nameof(FicheTracabiliteId))]
    public class TracerFicheMeuble
    {
        [Column("id_fiche_tracabilite")]
        [Required]
        public int FicheTracabiliteId { get; set; }

        [Column("id_meuble")]
        [Required]
        public int MeubleId { get; set; }

        [Column("quantite")]
        [Required]
        public int Quantite { get; set; }

        [ForeignKey("FicheTracabiliteId")]
        public virtual FicheTracabilite? FicheTracabilite { get; set; }

        [ForeignKey("MeubleId")]
        public virtual Meuble? Meuble { get; set; }
    }
}
