using OzelDersler.Entity.Concrete;
using OzelDersler.Web.Models;

namespace OzelDersler.Web.Areas.Admin.Models
{
    public class TeacherCourseDto
    {
        public int CourseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Experience { get; set; }
        public string CourseName { get; set; }
        public string BranchName { get; set; }
        public string CourseDescription { get; set; }
        public decimal PricePerHour { get; set; }
        public string BranchImageUrl { get; set; }
        public string CourseUrl { get; set; }

    }
}
