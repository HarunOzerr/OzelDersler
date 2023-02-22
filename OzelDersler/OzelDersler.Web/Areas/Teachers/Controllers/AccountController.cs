using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OzelDersler.Business.Abstract;
using OzelDersler.Business.Concrete;
using OzelDersler.Entity.Concrete;
using OzelDersler.Entity.Concrete.Identity;
using OzelDersler.Web.Models;

namespace OzelDersler.Web.Areas.Teachers.Controllers
{
    [Area("Teachers")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITeacherService _teacherManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITeacherService teacherManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _teacherManager = teacherManager;
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto, string registerType)
        {
            if (ModelState.IsValid)
            {
                if (registerType == "teacher")
                {
                    var user = new User
                    {
                        UserName = registerDto.UserName,
                        Email = registerDto.Email,
                        Teachers = new Teacher
                        {
                            FirstName = registerDto.FirstName,
                            LastName = registerDto.LastName
                        }
                    };
                    var result = await _userManager.CreateAsync(user, registerDto.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Teacher");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    var user = new User
                    {
                        UserName = registerDto.UserName,
                        Email = registerDto.Email,
                        Students = new Student
                        {
                            FirstName = registerDto.FirstName,
                            LastName = registerDto.LastName
                        }
                    };
                    var result = await _userManager.CreateAsync(user, registerDto.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Student");
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(registerDto);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
        public IActionResult Manage(string id)
        {
            var name = id;
            if (String.IsNullOrEmpty(name))
            {
                return NotFound();
            }
            var user = _teacherManager.GetUser(name, "");
            if (user == null) { return NotFound(); }
            UserManageDto userManageDto = new UserManageDto();
            if (User.IsInRole("Teacher"))
            {
                userManageDto.Id = user.Id;
                userManageDto.FirstName = user.Teachers.FirstName;
                userManageDto.LastName = user.Teachers.LastName;
                userManageDto.UserName = user.UserName;
                userManageDto.Email = user.Email;
                userManageDto.PhoneNumber = user.PhoneNumber;
                userManageDto.About = user.Teachers.About;
                userManageDto.Experience = user.Teachers.Experience;
                userManageDto.Gender = user.Teachers.Gender;
                userManageDto.DateOfBirth = user.Teachers.DateOfBirth;
            }
            else if (User.IsInRole("Student"))
            {
                userManageDto.Id = user.Id;
                userManageDto.FirstName = user.Students.FirstName;
                userManageDto.LastName = user.Students.LastName;
                userManageDto.UserName = user.UserName;
                userManageDto.Email = user.Email;
                userManageDto.PhoneNumber = user.PhoneNumber;
                userManageDto.Gender = user.Students.Gender;
                userManageDto.DateOfBirth = user.Students.DateOfBirth;
            }
            else
            {
                userManageDto.Id = user.Id;
                userManageDto.UserName = user.UserName;
                userManageDto.Email = user.Email;
                userManageDto.PhoneNumber = user.PhoneNumber;
            }
            return View(userManageDto);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(UserManageDto userManageDto)
        {
            if (userManageDto == null) { return NotFound(); }
            var user = _teacherManager.GetUser("", userManageDto.Id);
            if (User.IsInRole("Teacher"))
            {
                if (user == null) { return NotFound(); }
                user.Teachers.FirstName = userManageDto.FirstName;
                user.Teachers.LastName = userManageDto.LastName;
                user.UserName = userManageDto.UserName;
                user.Email = userManageDto.Email;
                user.PhoneNumber = userManageDto.PhoneNumber;
                user.Teachers.About = userManageDto.About;
                user.Teachers.Experience = userManageDto.Experience;
                user.Teachers.Gender = userManageDto.Gender;
                user.Teachers.DateOfBirth = userManageDto.DateOfBirth;
            }
            else if (User.IsInRole("Student"))
            {
                if (user == null) { return NotFound(); }
                user.Students.FirstName = userManageDto.FirstName;
                user.Students.LastName = userManageDto.LastName;
                user.UserName = userManageDto.UserName;
                user.Email = userManageDto.Email;
                user.PhoneNumber = userManageDto.PhoneNumber;
                user.Students.Gender = userManageDto.Gender;
                user.Students.DateOfBirth = userManageDto.DateOfBirth;
            }
            else
            {
                user.PhoneNumber = userManageDto.PhoneNumber;
                user.UserName = userManageDto.UserName;
                user.Email = userManageDto.Email;
            }
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index", "Home");
        }

    }
}
