using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models
{
    public class Training
    {
        [Key]
        public string? training_id { get; set; }

        [Required]
        public string? training_name { get; set; }

        [Required]
        public DateTime? start_date { get; set; }

        [Required]
        public int? duration { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string? training_link { get; set; }
    }
}