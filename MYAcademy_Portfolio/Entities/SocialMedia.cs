namespace MYAcademy_Portfolio.Entities
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }
        public string? MediaName { get; set; }
        public string? MediaUrl { get; set; }
        public string? MediaIcon { get; set; }
        public bool IsActive { get; set; } 
    }
}
