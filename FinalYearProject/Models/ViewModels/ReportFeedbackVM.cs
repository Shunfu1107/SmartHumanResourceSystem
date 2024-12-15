namespace FinalYearProject.Models.ViewModels
{
    public class ReportFeedbackVM
    {
        public Report Report { get; set; }
        public List<ReportFeedback> Feedbacks { get; set; }
        public List<Attachment> Attachments { get; set; }
        public string? CurrentUserId { get; set; }
        public string? FeedbackContent { get; set; }
    }
}
