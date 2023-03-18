using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("livrer_meuble_livraison")]
    [PrimaryKey(nameof(MeubleId), nameof(LivraisonId))]
    public class LivrerMeubleLivraison
    {
        [Column("id_meuble")]
        public int MeubleId { get; set; }

        [Column("id_livraison")]
        public int? LivraisonId { get; set; }

        [Column("quantite_livree")]
        public int QuantiteLivree { get; set; }

        [ForeignKey("MeubleId")]
        public Meuble? Meuble { get; set; }

        [ForeignKey("LivraisonId")]
        public  Livraison? Livraison { get; set; }
    }
}
