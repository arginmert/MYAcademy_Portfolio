using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolio.Models
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Ad soyad 3-100 karakter olmalıdır.")]
        public string NameSurname { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(150, ErrorMessage = "E-posta en fazla 150 karakter olabilir.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Konu zorunludur.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Konu 3-150 karakter olmalıdır.")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mesaj zorunludur.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Mesaj 10-2000 karakter olmalıdır.")]
        public string MessageDetail { get; set; } = string.Empty;
    }
}