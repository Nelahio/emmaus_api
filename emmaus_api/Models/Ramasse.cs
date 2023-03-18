using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emmaus_api.Models
{
    [Table("ramasse")]
    public class Ramasse
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("nom_donateur")]
        public string NomDonateur { get; set; }

        [StringLength(50)]
        [Column("prenom_donateur")]
        public string PrenomDonateur { get; set; }

        [Required]
        [StringLength(50)]
        [Column("adresse_donateur")]
        public string AdresseDonateur { get; set; }

        [Required]
        [StringLength(5)]
        [Column("code_postal_donateur")]
        public string CodePostalDonateur { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ville_donateur")]
        public string VilleDonateur { get; set; }

        [Required]
        [StringLength(20)]
        [Column("telephone_donateur")]
        public string TelephoneDonateur { get; set; }

        [Required]
        [StringLength(50)]
        [Column("mail_donateur")]
        public string MailDonateur { get; set; }

        [Required]
        [Column("date_ramasse")]
        public DateTime DateRamasse { get; set; }

        [Required]
        [Column("id_tranche_horaire")]
        public int TrancheHoraireId { get; set; }

        [ForeignKey("TrancheHoraireId")]
        public TrancheHoraire? TrancheHoraire { get; set; }
    }

}
