namespace FinalYearProject.Models.ViewModels
{
    public class RatingListVM
    {
        public IEnumerable<EmployeeRatingVM> employeesRating { get; set; }

        public EmployeeRatingVM employerRating { get; set; }

        public string currentUser_id { get; set; }
    }
}
