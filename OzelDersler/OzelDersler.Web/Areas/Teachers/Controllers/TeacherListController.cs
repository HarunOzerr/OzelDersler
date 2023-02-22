using Microsoft.AspNetCore.Mvc;
using OzelDersler.Business.Abstract;
using OzelDersler.Entity.Concrete;
using OzelDersler.Web.Models;

namespace OzelDersler.Web.Areas.Teachers.Controllers
{
    [Area("Teachers")]
    public class TeacherListController : Controller
    {
        private readonly ITeacherService _teacherManager;

        public TeacherListController(ITeacherService teacherManager)
        {
            _teacherManager = teacherManager;
        }

        public async Task<IActionResult> TeacherList(string branchurl)
        {
            List<Teacher> teachers = await _teacherManager.GetTeachersByBranchAsync(branchurl);
            List<TeacherDto> teacherDtos = new List<TeacherDto>();
            foreach (var teacher in teachers)
            {
                teacherDtos.Add(new TeacherDto
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    PricePerHour = teacher.PricePerHour,
                    About = teacher.About,
                    ImageUrl = teacher.ImageUrl,
                    City = teacher.City,
                    Experience = teacher.Experience,
                });
            }
            return View(teacherDtos);
        }
    }
}
