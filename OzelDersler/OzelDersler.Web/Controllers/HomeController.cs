using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using OzelDersler.Business.Abstract;
using OzelDersler.Business.Concrete;
using OzelDersler.Entity.Concrete;
using OzelDersler.Web.Areas.Teachers.Models;
using OzelDersler.Web.Models;

namespace OzelDersler.Web.Controllers;

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
        if (User.IsInRole("Teacher"))
        {
            return Redirect("/Teachers");
        }
        else if (User.IsInRole("Student"))
        {
            return Redirect("/Students");
        }

        List<TeacherCourse> teacherCourses = await _courseService.GetTeacherCoursesWithBranches();
        List<TeacherCourseDto> teacherCourseDtos = new List<TeacherCourseDto>();
        List<Teacher> teachers = await _teacherService.GetHomePageTeachersAsync();
        List<TeacherDto> teacherDtos = new List<TeacherDto>();
        foreach (var teacher in teachers)
        {
            teacherDtos.Add(new TeacherDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                ImageUrl = teacher.ImageUrl,
                City = teacher.City,
                About = teacher.About,
                PricePerHour = teacher.PricePerHour,
                Experience = teacher.Experience,
                Gender= teacher.Gender
            });
        }
        foreach (var teacherCourse in teacherCourses)
        {
            teacherCourseDtos.Add(new TeacherCourseDto
            {
                FirstName = teacherCourse.Teacher.FirstName,
                LastName = teacherCourse.Teacher.LastName,
                BranchName = teacherCourse.Course.Branch.BranchName,
                BranchImageUrl = teacherCourse.Course.Branch.ImageUrl,
                CourseName = teacherCourse.Course.Name,
                CourseDescription = teacherCourse.Course.Description,
                CourseUrl = teacherCourse.Course.Url,
                PricePerHour = teacherCourse.Course.PricePerHour,
                Experience = teacherCourse.Teacher.Experience
            });
        }
        var model = new TeacherAndTeacherCourseViewModel()
        {
            Teachers = teacherDtos,
            teacherCourseDtos = teacherCourseDtos
        };
        return View(model);
    }
    public IActionResult Detail(int id)
    {
        var teacher = _teacherService.GetTeacherWithUser(id);
        var teacherDetailDto = new TeacherDetailDto
        {
            Id = teacher.Id,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            About = teacher.About,
            Address = teacher.Address,
            City = teacher.City,
            Experience = teacher.Experience,
            Gender = teacher.Gender,
            ImageUrl = teacher.ImageUrl,
            Job = teacher.Job,
            EMail = teacher.User.Email,
            PhoneNumber = teacher.User.PhoneNumber
        };
        return View(teacherDetailDto);
    }


}
