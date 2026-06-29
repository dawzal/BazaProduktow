using System.ComponentModel.DataAnnotations;

namespace BazaProduktow.Models
{
    public class Produkt
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [StringLength(100, ErrorMessage = "Nazwa nie może być dłuższa niż 100 znaków")]
        [Display(Name = "Nazwa")]
        public string Nazwa { get; set; } = string.Empty;

        [Display(Name = "Opis")]
        [StringLength(500, ErrorMessage = "Opis nie może być dłuższy niż 500 znaków")]
        public string Opis { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cena jest wymagana")]
        [Range(0.01, 999999, ErrorMessage = "Cena musi być większa niż 0")]
        [Display(Name = "Cena")]
        public decimal Cena { get; set; }

        [Required(ErrorMessage = "Kategoria jest wymagana")]
        [Display(Name = "Kategoria")]
        public string Kategoria { get; set; } = string.Empty;

        [Display(Name = "Data dodania")]
        public DateTime DataDodania { get; set; } = DateTime.Now;
    }
}