using OzelDersler.Entity.Concrete;
using OzelDersler.Web.Areas.Teachers.Models;

namespace OzelDersler.Web.Models
{
    public class TeacherAndTeacherCourseViewModel
    {
        public List<TeacherCourseDto> teacherCourseDtos { get; set; }
        public List<TeacherDto> Teachers { get; set; }
    }
}
