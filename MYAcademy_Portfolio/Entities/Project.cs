namespace MYAcademy_Portfolio.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string GithubUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
