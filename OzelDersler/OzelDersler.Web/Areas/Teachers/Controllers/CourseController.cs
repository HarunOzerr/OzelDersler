using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OzelDersler.Business.Abstract;
using OzelDersler.Core;
using OzelDersler.Entity.Concrete;
using OzelDersler.Entity.Concrete.Identity;
using OzelDersler.Web.Areas.Teachers.Models;

namespace OzelDersler.Web.Areas.Teachers.Controllers
{
    [Area("Teachers")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseManager;
        private readonly UserManager<User> _userManager;
        private readonly ITeacherService _teacherManager;
        private readonly IBranchService _branchManager;

        public CourseController(ICourseService courseManager, UserManager<User> userManager, ITeacherService teacherManager, IBranchService branchManager)
        {
            _courseManager = courseManager;
            _userManager = userManager;
            _teacherManager = teacherManager;
            _branchManager = branchManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CourseList(string branchUrl)
        {
            List<StudentCourse> studentCourses = await _courseManager.GetStudentCoursesByBranchAsync(branchUrl);
            List<StudentCourseDto> studentCourseDtos = new List<StudentCourseDto>();
            foreach (var studentCourse in studentCourses)
            {
                studentCourseDtos.Add(new StudentCourseDto
                {
                    FirstName = studentCourse.Student.FirstName,
                    LastName = studentCourse.Student.LastName,
                    BranchName = studentCourse.Course.Branch.BranchName,
                    BranchImageUrl = studentCourse.Course.Branch.ImageUrl,
                    CourseName = studentCourse.Course.Name,
                    CourseDescription = studentCourse.Course.Description,
                    CourseUrl = studentCourse.Course.Url,
                    PricePerHour = studentCourse.Course.PricePerHour
                });
            }
            return View(studentCourseDtos);
        }

        public async Task<IActionResult> AddCourse(string id)
        {
            var name = id;
            var branches = await _branchManager.GetAllAsync();
            var courseAddDto = new CourseAddDto
            {
                Branches = branches,
                UserName = name
            };
            return View(courseAddDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseAddDto courseAddDto)
        {
            if (ModelState.IsValid)
            {
                if (courseAddDto == null)
                {
                    return NotFound();
                }
                var user = _teacherManager.GetUser(courseAddDto.UserName, "");
                var teacher = await _teacherManager.GetTeacherWithCourses(user.Teachers.Id);
                TeacherCourse teacherCourse = new TeacherCourse
                {
                    Course = new Course
                    {
                        Name = courseAddDto.CourseName,
                        Description = courseAddDto.CourseDescription,
                        PricePerHour= courseAddDto.PricePerHour,
                        BranchId = courseAddDto.SelectedBranchId,
                        Url = Jobs.InitUrl(courseAddDto.CourseName)
                    },
                    TeacherId= teacher.Id
                };
                teacher.TeachersCourses.Add(teacherCourse);
                _teacherManager.Update(teacher);
            }
            var branches = await _branchManager.GetAllAsync();
            courseAddDto.Branches = branches;
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CourseDetails(string courseUrl)
        {
            if (courseUrl == null)
            {
                return NotFound();
            }
            var studentCourse = await _courseManager.GetStudentCourseDetailsByUrlAsync(courseUrl);
            StudentCourseDetailDto studentCourseDetailDto = new StudentCourseDetailDto
            {
                FirstName = studentCourse.Student.FirstName,
                LastName = studentCourse.Student.LastName,
                BranchName = studentCourse.Course.Branch.BranchName,
                BranchImageUrl = studentCourse.Course.Branch.ImageUrl,
                CourseId = studentCourse.CourseId,
                CourseName = studentCourse.Course.Name,
                CourseDescription = studentCourse.Course.Description,
                PricePerHour = studentCourse.Course.PricePerHour,
                Email = studentCourse.Student.User.Email,
                Phone = studentCourse.Student.User.PhoneNumber,
                Address = studentCourse.Student.Address,
            };
            return View(studentCourseDetailDto);
        }

        public async Task<IActionResult> TeacherCourseList()
        {
            List<TeacherCourse> teacherCourses = await _courseManager.GetTeacherCoursesWithBranches();
            List<TeacherCourseDto> teacherCourseDtos = new List<TeacherCourseDto>();
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
                    PricePerHour = teacherCourse.Course.PricePerHour
                });
            }
            return View(teacherCourseDtos);
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
                Experience = teacherCourse.Teacher.Experience,
                Phone = teacherCourse.Teacher.User.PhoneNumber,
                Email = teacherCourse.Teacher.User.Email,
                About = teacherCourse.Teacher.About
            };
            return View(teacherCourseDetailDto);
        }

        public async Task<IActionResult> MyCourses(string id)
        {
            var name = id;
            var user = _teacherManager.GetUser(name, "");
            List<TeacherCourse> teacherCourses = await _courseManager.GetTeacherCoursesByTeacherId(user.Teachers.Id);
            List<TeacherCourseDto> teacherCourseDtos = new List<TeacherCourseDto>();
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
                    CourseId = teacherCourse.CourseId
                });
            }
            return View(teacherCourseDtos);
        }

        public async Task<IActionResult> TeacherCourseEdit(int id)
        {
            var branches = await _branchManager.GetAllAsync();
            var teacherCourse = await _courseManager.GetTeacherCourseWithBranche(id);
            TeacherCourseUpdateDto teacherCourseUpdateDto = new TeacherCourseUpdateDto
            {
                Id = teacherCourse.CourseId,
                CourseName = teacherCourse.Course.Name,
                CourseDescription = teacherCourse.Course.Description,
                SelectedBranchId = teacherCourse.Course.BranchId,
                PricePerHour = teacherCourse.Course.PricePerHour,
                Branches = branches,
                TeacherId = teacherCourse.TeacherId,
                UserName = teacherCourse.Teacher.User.UserName
            };
            return View(teacherCourseUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> TeacherCourseEdit(TeacherCourseUpdateDto teacherCourseUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var teacher = await _teacherManager.GetTeacherWithCourses(teacherCourseUpdateDto.TeacherId);
                if (teacher == null)
                {
                    return NotFound();
                }
                var teacherCourse = teacher.TeachersCourses.Where(tc => tc.CourseId == teacherCourseUpdateDto.Id).FirstOrDefault();
                teacherCourse.Course.Name = teacherCourseUpdateDto.CourseName;
                teacherCourse.Course.Description = teacherCourseUpdateDto.CourseDescription;
                teacherCourse.Course.PricePerHour = teacherCourseUpdateDto.PricePerHour;
                teacherCourse.Course.BranchId = teacherCourseUpdateDto.SelectedBranchId;
                _teacherManager.Update(teacher);
            }
            var branches = await _branchManager.GetAllAsync();
            teacherCourseUpdateDto.Branches = branches;
            return Redirect("/Teachers/Course/MyCourses/" + teacherCourseUpdateDto.UserName);
        }

        public async Task<IActionResult> TeacherCourseDelete(int id)
        {
            var course = await _courseManager.GetByIdAsync(id);
            _courseManager.Delete(course);
            return RedirectToAction("Index", "Home");
        }
    }
}
