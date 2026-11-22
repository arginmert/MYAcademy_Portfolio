using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolio.Entities
{
    public class Setting
    {
        public int SettingId { get; set; }

        [Required(ErrorMessage = "Site başlığı zorunludur.")]
        [StringLength(70, ErrorMessage = "Site başlığı en fazla 70 karakter olabilir.")]
        public string SiteTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Site açıklaması zorunludur.")]
        [StringLength(160, ErrorMessage = "Site açıklaması en fazla 160 karakter olabilir.")]
        public string SiteDescription { get; set; } = string.Empty;

        public DateTime UpdatedDate { get; set; }
    }
}