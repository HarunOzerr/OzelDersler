using OzelDersler.Entity.Concrete;
using System.ComponentModel;

namespace OzelDersler.Web.Areas.Students.Models
{
    public class StudentCourseAddDto
    {
        public int StudentId { get; set; }
        public string UserId { get; set; }

        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [DisplayName("Kurs Adı")]
        public string CourseName { get; set; }

        [DisplayName("Kurs Açıklaması")]
        public string CourseDescription { get; set; }

        [DisplayName("Saatlik Ücret")]
        public decimal PricePerHour { get; set; }

        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string BranchName { get; set; }
        public StudentCourse StudentCourse { get; set; }

        [DisplayName("Branşlar")]
        public List<Branch> Branches { get; set; }
        public int SelectedBranchId { get; set; }
    }
}
