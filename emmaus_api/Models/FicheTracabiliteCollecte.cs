using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("fiche_tracabilite_collecte")]
    [PrimaryKey(nameof(FicheTracabiliteId), nameof(RamasseId))]
    public class FicheTracabiliteCollecte
    {
        [Column("id_fiche_tracabilite")]
        [Required]
        public int FicheTracabiliteId { get; set; }

        [Column("id_ramasse")]
        [Required]
        public int RamasseId { get; set; }

        [ForeignKey("FicheTracabiliteId")]
        public FicheTracabilite FicheTracabilite { get; set; }
        [ForeignKey("RamasseId")]
        public Ramasse Ramasse { get; set; }
    }
}
