using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("generer_declaration_fiche")]
    [PrimaryKey(nameof(DeclarationId), nameof(FicheTracabiliteId))]
    public class GenererDeclarationFiche
    {
        [Column("id_declaration")]
        [Required]
        public string DeclarationId { get; set; }

        [Column("id_fiche_tracabilite")]
        [Required]
        public int FicheTracabiliteId { get; set; }

        [ForeignKey("DeclarationId")]
        public virtual Declaration Declaration { get; set; }

        [ForeignKey("FicheTracabiliteId")]
        public virtual FicheTracabilite FicheTracabilite { get; set; }
    }
}
