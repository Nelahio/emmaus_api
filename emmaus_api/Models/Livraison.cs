using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("livraison")]
    public class Livraison
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("id_tranche_horaire")]
        public int TrancheHoraireId { get; set; }

        [Required]
        [Column("nom_client")]
        public string NomClient { get; set; }

        [Column("prenom_client")]
        public string PrenomClient { get; set; }

        [Required]
        [Column("adresse_client")]
        public string AdresseClient { get; set; }

        [Required]
        [Column("code_postal_client")]
        [StringLength(5)]
        public string CodePostalClient { get; set; }

        [Required]
        [Column("ville_client")]
        public string VilleClient { get; set; }

        [Required]
        [StringLength(20)]
        [Column("telephone_client")]
        public string TelephoneClient { get; set; }

        [Required]
        [StringLength(50)]
        [Column("mail_client")]
        public string MailClient { get; set; }

        [Required]
        [Column("date_livraison")]
        public DateTime DateLivraison { get; set; }

        [Required]
        [Column("id_fiche_tracabilite_reemploi")]
        public int FicheTracabiliteReemploiId { get; set; }

        [ForeignKey("TrancheHoraireId")]
        public TrancheHoraire? TrancheHoraire { get; set; }

        [ForeignKey("FicheTracabiliteReemploiId")]
        public FicheTracabiliteReemploi? FicheTracabiliteReemploi { get; set; }
    }
}
