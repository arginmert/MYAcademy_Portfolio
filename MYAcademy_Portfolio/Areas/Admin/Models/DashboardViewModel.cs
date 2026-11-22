using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Models
{
    public class ChartItem
    {
        public string Label { get; set; } = string.Empty;
        public int Value { get; set; }
    }

    public class DashboardViewModel
    {
        // Kartlar
        public int ProjectCount { get; set; }
        public int CategoryCount { get; set; }
        public int SkillCount { get; set; }
        public int ExperienceCount { get; set; }
        public int EducationCount { get; set; }
        public int MessageCount { get; set; }
        public int UnreadCount { get; set; }
        public int ReadCount => MessageCount - UnreadCount;
        public int MessagesThisMonth { get; set; }

        // Grafikler
        public List<ChartItem> ProjectsByCategory { get; set; } = new();
        public List<ChartItem> MonthlyMessages { get; set; } = new();

        // Site tamamlanma kontrolleri
        public bool HasAbout { get; set; }
        public bool HasCv { get; set; }
        public int ProjectsWithoutGithub { get; set; }
        public int EmptyCategoryCount { get; set; }

        public List<Message> LastMessages { get; set; } = new();
        public int ActiveProjectCount { get; set; }
        public List<Project> LastProjects { get; set; } = new();
    }
}