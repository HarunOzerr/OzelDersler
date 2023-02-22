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
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IBranchService _branchService;
        private readonly UserManager<User> _userManager;

        public TeacherController(ITeacherService teacherService, IBranchService branchService, UserManager<User> userManager)
        {
            _teacherService = teacherService;
            _branchService = branchService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var teachers = _teacherService.GetTeachersWithUsers();
            var teacherListDto = teachers
                .Select(t => new TeacherListDto
                {
                    Teacher = t,
                    User = t.User
                }).ToList();
            return View(teacherListDto);
        }

        public async Task<IActionResult> Create()
        {
            var branches = await _branchService.GetAllAsync();
            var teacherAddDto = new TeacherAddDto
            {
                Branches = branches
            };
            return View(teacherAddDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherAddDto teacherAddDto)
        {
            if (ModelState.IsValid)
            {
                var teacher = new Teacher
                {
                    FirstName = teacherAddDto.FirstName,
                    LastName = teacherAddDto.LastName,
                    User = new User
                    {
                        UserName = teacherAddDto.UserName,
                        Email = teacherAddDto.Email
                    }
                };
                await _teacherService.CreateTeacherAsync(teacher, teacherAddDto.SelectedBranchIds);
                return RedirectToAction("Index");
            }
            var branches = await _branchService.GetAllAsync();
            teacherAddDto.Branches = branches;
            return View(teacherAddDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _teacherService.GetTeacherWithBranches(id);
            TeacherUpdateDto teacherUpdateDto = new TeacherUpdateDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Branches = await _branchService.GetAllAsync(),
                SelectedBranchIds = teacher.TeacherBranches.Select(tb => tb.BranchId).ToArray(),
                Email = teacher.User.Email,
                UserName = teacher.User.UserName
            };
            return View(teacherUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TeacherUpdateDto teacherUpdateDto, int[] selectedBranchIds)
        {
            if (ModelState.IsValid)
            {
                var teacher = await _teacherService.GetTeacherWithBranches(teacherUpdateDto.Id);
                if (teacher == null)
                {
                    return NotFound();
                }
                var url = Jobs.InitUrl(teacherUpdateDto.FirstName + " " + teacherUpdateDto.LastName);
                teacher.FirstName = teacherUpdateDto.FirstName;
                teacher.LastName = teacherUpdateDto.LastName;
                teacher.User.Email = teacherUpdateDto.Email;
                teacher.User.UserName = teacherUpdateDto.UserName;
                teacher.Url = url;
                await _teacherService.UpdateTeacherAsync(teacher, selectedBranchIds);
                return RedirectToAction("Index");
            }
            var branches = await _branchService.GetAllAsync();
            teacherUpdateDto.Branches = branches;

            return View(teacherUpdateDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _teacherService.GetTeacherWithBranches(id);
            if (teacher == null) { return NotFound(); }
            _teacherService.Delete(teacher);
            await _userManager.DeleteAsync(teacher.User);
            return RedirectToAction("Index");
        }
    }
}
