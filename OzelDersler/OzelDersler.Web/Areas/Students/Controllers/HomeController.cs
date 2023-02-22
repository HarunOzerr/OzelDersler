using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OzelDersler.Business.Abstract;
using OzelDersler.Entity.Concrete;
using OzelDersler.Web.Areas.Teachers.Models;
using OzelDersler.Web.Models;

namespace OzelDersler.Web.Areas.Students.Controllers;
[Area("Students")]
public class HomeController : Controller
{
    private readonly ITeacherService _teacherService;
    private readonly ICourseService _courseService;


    public HomeController(ITeacherService teacherService, ICourseService courseService)
    {
        _teacherService = teacherService;
        _courseService = courseService;
    }

    public async Task<IActionResult> Index()
    {
        var teacherCourses = await _courseService.GetTeacherCoursesWithBranches();
        List<TeacherCourseDto> teacherCourseDtos = new List<TeacherCourseDto>();
        foreach (var teacherCourse in teacherCourses)
        {
            teacherCourseDtos.Add(new TeacherCourseDto
            {
                FirstName = teacherCourse.Teacher.FirstName,
                LastName = teacherCourse.Teacher.LastName,
                CourseName = teacherCourse.Course.Name,
                CourseDescription = teacherCourse.Course.Description,
                CourseUrl = teacherCourse.Course.Url,
                PricePerHour = teacherCourse.Course.PricePerHour,
                BranchName = teacherCourse.Course.Branch.BranchName,
                BranchImageUrl = teacherCourse.Course.Branch.ImageUrl
            });
        }
        return View(teacherCourseDtos);
    }
    public IActionResult Detail()
    {
        return View();
    }


}
