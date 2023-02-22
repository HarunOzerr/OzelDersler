using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OzelDersler.Business.Abstract;
using OzelDersler.Entity.Concrete;
using OzelDersler.Web.Areas.Teachers.Models;
using OzelDersler.Web.Models;

namespace OzelDersler.Web.Areas.Teachers.Controllers
{
    [Area("Teachers")]
    public class HomeController : Controller
    {
        private readonly ICourseService _courseService;


        public HomeController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var studentCourses = await _courseService.GetStudentCoursesWithBranches();
            List<StudentCourseDto> studentCourseDtos= new List<StudentCourseDto>();
            foreach (var studentCourse in studentCourses)
            {
                studentCourseDtos.Add(new StudentCourseDto
                {
                    FirstName = studentCourse.Student.FirstName,
                    LastName = studentCourse.Student.LastName,
                    CourseName = studentCourse.Course.Name,
                    CourseDescription = studentCourse.Course.Description,
                    CourseUrl = studentCourse.Course.Url,
                    PricePerHour = studentCourse.Course.PricePerHour,
                    BranchName = studentCourse.Course.Branch.BranchName,
                    BranchImageUrl = studentCourse.Course.Branch.ImageUrl
                });
            }
            return View(studentCourseDtos);
        }
        public IActionResult Detail()
        {
            return View();
        }


    }
}
