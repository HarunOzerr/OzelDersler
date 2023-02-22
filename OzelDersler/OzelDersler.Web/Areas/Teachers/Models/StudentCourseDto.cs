using OzelDersler.Entity.Concrete;

namespace OzelDersler.Web.Areas.Teachers.Models
{
    public class StudentCourseDto
    {
        public int CourseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CourseName { get; set; }
        public string BranchName { get; set; }
        public string CourseDescription { get; set; }
        public decimal PricePerHour { get; set; }
        public string BranchImageUrl { get; set; }
        public string CourseUrl { get; set; }


    }
}
