using Microsoft.AspNetCore.Mvc;
using OzelDersler.Business.Abstract;
using OzelDersler.Business.Concrete;
using OzelDersler.Core;
using OzelDersler.Entity.Concrete;
using OzelDersler.Web.Areas.Admin.Models;

namespace OzelDersler.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IBranchService _branchService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;

        public CourseController(ICourseService courseService, IBranchService branchService, ITeacherService teacherService, IStudentService studentService)
        {
            _courseService = courseService;
            _branchService = branchService;
            _teacherService = teacherService;
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TeachersCourses()
        {
            var teacherCourses = await _courseService.GetTeacherCoursesWithBranches();
            List<TeacherCourseDto> teacherCourseDtos = new List<TeacherCourseDto>();
            foreach (var teacherCourse in teacherCourses)
            {
                teacherCourseDtos.Add(new TeacherCourseDto
                {
                    CourseId = teacherCourse.CourseId,
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

        public async Task<IActionResult> TeacherCourseEdit(int id)
        {
            var branches = await _branchService.GetAllAsync(); 
            var teacherCourse = await _courseService.GetTeacherCourseWithBranche(id);
            TeacherCourseUpdateDto teacherCourseUpdateDto = new TeacherCourseUpdateDto
            {
                Id = teacherCourse.CourseId,
                CourseName= teacherCourse.Course.Name,
                CourseDescription= teacherCourse.Course.Description,
                SelectedBranchId = teacherCourse.Course.BranchId,
                PricePerHour = teacherCourse.Course.PricePerHour,
                Branches= branches,
                TeacherId = teacherCourse.TeacherId
            };
            return View(teacherCourseUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> TeacherCourseEdit(TeacherCourseUpdateDto teacherCourseUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var teacher = await _teacherService.GetTeacherWithCourses(teacherCourseUpdateDto.TeacherId);
                if (teacher == null)
                {
                    return NotFound();
                }
                var teacherCourse = teacher.TeachersCourses.Where(tc => tc.CourseId == teacherCourseUpdateDto.Id).FirstOrDefault();
                teacherCourse.Course.Name = teacherCourseUpdateDto.CourseName;
                teacherCourse.Course.Description= teacherCourseUpdateDto.CourseDescription;
                teacherCourse.Course.PricePerHour = teacherCourseUpdateDto.PricePerHour;
                teacherCourse.Course.BranchId = teacherCourseUpdateDto.SelectedBranchId;
                _teacherService.Update(teacher);
            }
            var branches = await _branchService.GetAllAsync();
            teacherCourseUpdateDto.Branches= branches;
            return RedirectToAction("TeachersCourses", "Course");
        }

        public async Task<IActionResult> AddTeacherCourse()
        {
            var branches = await _branchService.GetAllAsync();
            var teacherCourseAddDto = new TeacherCourseAddDto
            {
                Branches = branches
            };
            return View(teacherCourseAddDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacherCourse(TeacherCourseAddDto teacherCourseAddDto)
        {
            var teacherCourse = new TeacherCourse
            {
                Course = new Course
                {
                    Name = teacherCourseAddDto.CourseName,
                    BranchId = teacherCourseAddDto.SelectedBranchId,
                    Description = teacherCourseAddDto.CourseDescription,
                    PricePerHour = teacherCourseAddDto.PricePerHour,
                    Url = Jobs.InitUrl(teacherCourseAddDto.CourseName)
                },
                TeacherId = teacherCourseAddDto.TeacherId
            };
            var teacher = await _teacherService.GetTeacherWithCourses(teacherCourseAddDto.TeacherId);
            teacher.TeachersCourses.Add(teacherCourse);
            _teacherService.Update(teacher);
            var branches = await _branchService.GetAllAsync();
            teacherCourseAddDto.Branches = branches;
            return RedirectToAction("TeachersCourses", "Course");
        }

        public async Task<IActionResult> DeleteTeacherCourse(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            _courseService.Delete(course);
            return RedirectToAction("TeachersCourses", "Course");
        }

        public async Task<IActionResult> StudentsCourses()
        {
            List<StudentCourse> studentCourses = await _courseService.GetStudentCoursesWithBranches();
            List<StudentCourseDto> studentCourseDtos = new List<StudentCourseDto>();
            foreach (var studentCourse in studentCourses)
            {
                studentCourseDtos.Add(new StudentCourseDto
                {
                    CourseId = studentCourse.CourseId,
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

        public async Task<IActionResult> AddStudentCourse()
        {
            var branches = await _branchService.GetAllAsync();
            var courseAddDto = new StudentCourseAddDto
            {
                Branches = branches
            };
            return View(courseAddDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentCourse(StudentCourseAddDto studentCourseAddDto)
        {
            var studentCourse = new StudentCourse
            {
                Course = new Course
                {
                    Name = studentCourseAddDto.CourseName,
                    BranchId = studentCourseAddDto.SelectedBranchId,
                    Description = studentCourseAddDto.CourseDescription,
                    PricePerHour = studentCourseAddDto.PricePerHour,
                    Url = Jobs.InitUrl(studentCourseAddDto.CourseName)
                },
                StudentId = studentCourseAddDto.StudentId
            };
            var student = await _studentService.GetStudentWithCourse(studentCourseAddDto.StudentId);
            student.StudentsCourses.Add(studentCourse);
            _studentService.Update(student);
            var branches = await _branchService.GetAllAsync();
            studentCourseAddDto.Branches = branches;
            return RedirectToAction("StudentsCourses", "Course");
        }

        public async Task<IActionResult> StudentCourseEdit(int id)
        {
            var branches = await _branchService.GetAllAsync();
            var studentCourse = await _courseService.GetStudentCourseWithBranchByCourseId(id);
            StudentCourseUpdateDto teacherCourseUpdateDto = new StudentCourseUpdateDto
            {
                Id = studentCourse.CourseId,
                CourseName = studentCourse.Course.Name,
                CourseDescription = studentCourse.Course.Description,
                SelectedBranchId = studentCourse.Course.BranchId,
                PricePerHour = studentCourse.Course.PricePerHour,
                Branches = branches,
                StudentId = studentCourse.StudentId
            };
            return View(teacherCourseUpdateDto);
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
                var teacherCourse = student.StudentsCourses.Where(tc => tc.CourseId == studentCourseUpdateDto.Id).FirstOrDefault();
                teacherCourse.Course.Name = studentCourseUpdateDto.CourseName;
                teacherCourse.Course.Description = studentCourseUpdateDto.CourseDescription;
                teacherCourse.Course.PricePerHour = studentCourseUpdateDto.PricePerHour;
                teacherCourse.Course.BranchId = studentCourseUpdateDto.SelectedBranchId;
                _studentService.Update(student);
            }
            var branches = await _branchService.GetAllAsync();
            studentCourseUpdateDto.Branches = branches;
            return RedirectToAction("StudentsCourses", "Course");
        }
        public async Task<IActionResult> DeleteStudentCourse(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            _courseService.Delete(course);
            return RedirectToAction("StudentsCourses", "Course");
        }
    }
}
