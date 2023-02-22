using Microsoft.AspNetCore.Mvc;
using OzelDersler.Business.Abstract;
using OzelDersler.Business.Concrete;
using OzelDersler.Core;
using OzelDersler.Entity.Concrete;
using OzelDersler.Web.Areas.Students.Models;
using OzelDersler.Web.Areas.Teachers.Models;

namespace OzelDersler.Web.Areas.Students.Controllers
{
    [Area("Students")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly IBranchService _branchManager;
        private readonly ITeacherService _teacherService;

        public CourseController(ICourseService courseService, IStudentService studentService, IBranchService branchManager, ITeacherService teacherService)
        {
            _courseService = courseService;
            _studentService = studentService;
            _branchManager = branchManager;
            _teacherService = teacherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CourseList(string branchurl)
        {
            var teacherCourses = await _courseService.GetTeacherCoursesByBranchAsync(branchurl);
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

        public async Task<IActionResult> AddCourse(string id)
        {
            var name = id;
            var branches = await _branchManager.GetAllAsync();
            var courseAddDto = new StudentCourseAddDto
            {
                Branches = branches,
                UserName = name
            };
            return View(courseAddDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(StudentCourseAddDto courseAddDto)
        {
            if (ModelState.IsValid)
            {
                if (courseAddDto == null)
                {
                    return NotFound();
                }
                var user = await _studentService.GetStudentByUserName(courseAddDto.UserName);
                var student = await _studentService.GetStudentWithCourse(user.Id);
                StudentCourse studentCourse = new StudentCourse
                {
                    Course = new Course
                    {
                        Name = courseAddDto.CourseName,
                        Description = courseAddDto.CourseDescription,
                        PricePerHour = courseAddDto.PricePerHour,
                        BranchId = courseAddDto.SelectedBranchId,
                        Url = Jobs.InitUrl(courseAddDto.CourseName)
                    },
                    StudentId = student.Id
                };
                student.StudentsCourses.Add(studentCourse);
                _studentService.Update(student);
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
            var teacherCourse = await _courseService.GetTeacherCourseDetailsByUrlAsync(courseUrl);
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
                Experience = teacherCourse.Teacher.Experience,
                Address = teacherCourse.Teacher.Address,
                Phone = teacherCourse.Teacher.User.PhoneNumber,
                Email = teacherCourse.Teacher.User.Email,
                About = teacherCourse.Teacher.About
            };
            return View(teacherCourseDetailDto);
        }

        public async Task<IActionResult> MyCourses(string id)
        {
            var name = id;
            var user = _teacherService.GetUser(name, "");
            List<StudentCourse> studentCourses = await _courseService.GetStudentCoursesByTeacherId(user.Students.Id);
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
                    PricePerHour = studentCourse.Course.PricePerHour,
                    CourseId = studentCourse.CourseId
                });
            }
            return View(studentCourseDtos);
        }

        public async Task<IActionResult> StudentCourseEdit(int id)
        {
            var branches = await _branchManager.GetAllAsync();
            var studentCourse = await _courseService.GetStudentCourseWithBranchByCourseId(id);
            StudentCourseUpdateDto studentCourseUpdateDto = new StudentCourseUpdateDto
            {
                Id = studentCourse.CourseId,
                CourseName = studentCourse.Course.Name,
                CourseDescription = studentCourse.Course.Description,
                SelectedBranchId = studentCourse.Course.BranchId,
                PricePerHour = studentCourse.Course.PricePerHour,
                Branches = branches,
                StudentId = studentCourse.StudentId
            };
            return View(studentCourseUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> StudentCourseEdit(StudentCourseUpdateDto studentCourseUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var student = await _studentService.GetStudentWithCourse(studentCourseUpdateDto.StudentId);
                if (student == null)
                {
                    return NotFound();
                }
                var studentCourse = student.StudentsCourses.Where(tc => tc.CourseId == studentCourseUpdateDto.Id).FirstOrDefault();
                studentCourse.Course.Name = studentCourseUpdateDto.CourseName;
                studentCourse.Course.Description = studentCourseUpdateDto.CourseDescription;
                studentCourse.Course.PricePerHour = studentCourseUpdateDto.PricePerHour;
                studentCourse.Course.BranchId = studentCourseUpdateDto.SelectedBranchId;
                _studentService.Update(student);
            }
            var branches = await _branchManager.GetAllAsync();
            studentCourseUpdateDto.Branches = branches;
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> StudentCourseDelete(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            _courseService.Delete(course);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> StudentCourseDetails(string courseUrl)
        {
            if (courseUrl == null)
            {
                return NotFound();
            }
            var studentCourse = await _courseService.GetStudentCourseDetailsByUrlAsync(courseUrl);
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
                Address = studentCourse.Student.Address,
                Phone = studentCourse.Student.User.PhoneNumber,
                Email = studentCourse.Student.User.Email
            };
            return View(studentCourseDetailDto);
        }
    }

}
