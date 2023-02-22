using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OzelDersler.Business.Abstract;
using OzelDersler.Core;
using OzelDersler.Entity.Concrete;
using OzelDersler.Entity.Concrete.Identity;
using OzelDersler.Web.Areas.Admin.Models;

namespace OzelDersler.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly UserManager<User> _userManager;

        public StudentController(IStudentService studentService, UserManager<User> userManager)
        {
            _studentService = studentService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var students = _studentService.GetStudentsWithUser();
            var studentListDto = students
                .Select(s => new StudentListDto
                {
                    Student = s,
                    User = s.User
                }).ToList();
            return View(studentListDto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentAddDTo studentAddDTo)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    FirstName = studentAddDTo.FirstName,
                    LastName = studentAddDTo.LastName,
                    User = new User
                    {
                        UserName = studentAddDTo.UserName,
                        Email = studentAddDTo.Email,
                    }
                };
                await _studentService.CreateAsync(student);
                return RedirectToAction("Index");
            }
            return View(studentAddDTo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentWithUser(id);
            var studentUpdateDto = new StudentUpdateDto
            {
                Id = id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.User.Email,
                UserName = student.User.UserName
            };
            return View(studentUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentUpdateDto studentUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var student = await _studentService.GetStudentWithUser(studentUpdateDto.Id);
                if (student == null)
                {
                    return NotFound();
                }
                var url = Jobs.InitUrl(studentUpdateDto.FirstName + " " + studentUpdateDto.LastName);
                student.FirstName= studentUpdateDto.FirstName;
                student.LastName= studentUpdateDto.LastName;
                student.User.Email = studentUpdateDto.Email;
                student.User.UserName = studentUpdateDto.UserName;
                student.Url= url;
                _studentService.Update(student);
                return RedirectToAction("Index");
            }
            return View(studentUpdateDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentWithUser(id);
            if(student == null) { return NotFound(); }
            _studentService.Delete(student);
            await _userManager.DeleteAsync(student.User);
            return RedirectToAction("Index");
        }
    }
}
