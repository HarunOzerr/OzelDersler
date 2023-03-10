using OzelDersler.Entity.Concrete;
using System.ComponentModel;

namespace OzelDersler.Web.Areas.Admin.Models
{
    public class TeacherCourseUpdateDto
    {
        public int TeacherId { get; set; }
        public int Id { get; set; }

        [DisplayName("Kurs Adı")]
        public string CourseName { get; set; }

        [DisplayName("Kurs Açıklaması")]
        public string CourseDescription { get; set; }

        [DisplayName("Saatlik Ücret")]
        public decimal PricePerHour { get; set; }
        [DisplayName("Branşlar")]
        public List<Branch> Branches { get; set; }
        public int SelectedBranchId { get; set; }
    }
}
