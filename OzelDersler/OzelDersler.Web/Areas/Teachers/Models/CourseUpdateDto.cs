using OzelDersler.Entity.Concrete;
using System.ComponentModel;

namespace OzelDersler.Web.Areas.Teachers.Models
{
    public class CourseUpdateDto
    {
        public int TeacherId { get; set; }
        public string UserId { get; set; }

        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [DisplayName("Kurs Adı")]
        public string CourseName { get; set; }

        [DisplayName("Kurs Açıklaması")]
        public string CourseDescription { get; set; }

        [DisplayName("Saatlik Ücret")]
        public decimal PricePerHour { get; set; }

        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public string TeacherDescription { get; set; }
        public int TeacherExperience { get; set; }
        public string BranchName { get; set; }
        public TeacherCourse TeacherCourse { get; set; }

        [DisplayName("Branşlar")]
        public List<Branch> Branches { get; set; }
        public int SelectedBranchId { get; set; }
    }
}
