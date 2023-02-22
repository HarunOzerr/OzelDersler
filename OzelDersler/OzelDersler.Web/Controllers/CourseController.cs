using Microsoft.AspNetCore.Mvc;
using OzelDersler.Business.Abstract;
using OzelDersler.Business.Concrete;
using OzelDersler.Web.Areas.Teachers.Models;

namespace OzelDersler.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseManager;

        public CourseController(ICourseService courseManager)
        {
            _courseManager = courseManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TeacherCourseDetails(string courseUrl)
        {
            if (courseUrl == null)
            {
                return NotFound();
            }
            var teacherCourse = await _courseManager.GetTeacherCourseDetailsByUrlAsync(courseUrl);
            TeacherCourseDetailDto teacherCourseDetailDto = new TeacherCourseDetailDto
            {
                FirstName = teacherCourse.Teacher.FirstName,
                LastName = teacherCourse.Teacher.LastName,
                BranchName = teacherCourse.Course.Branch.BranchName,
                BranchImageUrl = teacherCourse.Course.Branch.ImageUrl,
                CourseId = teacherCourse.CourseId,
                CourseName = teacherCourse.Course.Name,
                CourseDescription = teacherCourse.Course.Description,
                PricePerHour = teacherCourse.Course.PricePerHour,
                Address = teacherCourse.Teacher.Address,
                Email = teacherCourse.Teacher.User.Email,
                Phone = teacherCourse.Teacher.User.PhoneNumber,
                Experience = teacherCourse.Teacher.Experience
            };
            return View(teacherCourseDetailDto);
        }
    }
}
