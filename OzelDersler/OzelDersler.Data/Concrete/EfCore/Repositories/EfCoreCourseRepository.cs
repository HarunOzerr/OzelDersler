using Microsoft.EntityFrameworkCore;
using OzelDersler.Data.Abstract;
using OzelDersler.Data.Concrete.EfCore.Contexts;
using OzelDersler.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Data.Concrete.EfCore.Repositories
{
    public class EfCoreCourseRepository : EfCoreGenericRepository<Course>, ICourseRepository
    {
        public EfCoreCourseRepository(DbContext context) : base(context)
        {

        }
        private OzelDerslerContext OzelDerslerContext
        {
            get { return _context as OzelDerslerContext; }
        }

        public async Task<List<StudentCourse>> GetStudentCoursesByBranchAsync(string branchUrl)
        {
            var courses = OzelDerslerContext
                .StudentsCourses
                .AsQueryable();
            if (branchUrl != null)
            {
                courses =
                     courses.Include(sc => sc.Course)
                     .ThenInclude(c => c.Branch)
                     .Include(sc => sc.Student)
                     .ThenInclude(s => s.User)
                    .Where(s => s.Course.Branch.Url == branchUrl);
            }
            return await courses.ToListAsync();
        }

        public async Task<List<StudentCourse>> GetStudentCoursesWithBranches()
        {
            var studentCourse = await OzelDerslerContext
                .StudentsCourses
                .Include(sc => sc.Course)
                .ThenInclude(c => c.Branch)
                .Include(sc => sc.Student)
                .ThenInclude(s => s.User)
                .ToListAsync();
            return studentCourse;
        }

        public async Task<StudentCourse> GetStudentCourseDetailsByUrlAsync(string courseUrl)
        {
            var studentCourse = await OzelDerslerContext
                .StudentsCourses
                .Include(sc => sc.Course)
                .ThenInclude(c => c.Branch)
                .Include(sc => sc.Student)
                .ThenInclude(s => s.User)
                .Where(sc => sc.Course.Url == courseUrl)
                .FirstOrDefaultAsync();
            return studentCourse;
        }

        public async Task<List<TeacherCourse>> GetTeacherCoursesByBranchAsync(string branchUrl)
        {
            var courses = OzelDerslerContext
                .TeachersCourses
                .AsQueryable();
            if (branchUrl != null)
            {
                courses =
                    courses.Include(tc => tc.Course)
                    .ThenInclude(c => c.Branch)
                    .Include(tc => tc.Teacher)
                    .ThenInclude(t => t.User)
                    .Where(tc => tc.Course.Branch.Url == branchUrl);
            }
            return await courses.ToListAsync();
        }

        public async Task<List<TeacherCourse>> GetTeacherCoursesWithBranches()
        {
            var courses = await OzelDerslerContext
                .TeachersCourses
                .Include(tc => tc.Teacher)
                .ThenInclude(t => t.User)
                .Include(tc => tc.Course)
                .ThenInclude(c => c.Branch)
                .ToListAsync();
            return courses;
        }
        public async Task<TeacherCourse> GetTeacherCourseDetailsByUrlAsync(string courseUrl)
        {
            var teacherCourse = await OzelDerslerContext
                .TeachersCourses
                .Include(tc => tc.Course)
                .ThenInclude(c => c.Branch)
                .Include(tc => tc.Teacher)
                .ThenInclude(t => t.User)
                .Where(tc => tc.Course.Url == courseUrl)
                .FirstOrDefaultAsync();
            return teacherCourse;
        }

        public async Task<List<TeacherCourse>> GetTeacherCoursesByTeacherId(int id)
        {
            var teacherCourse = await OzelDerslerContext
                .TeachersCourses
                .Where(tc => tc.TeacherId == id)
                .Include(tc => tc.Teacher)
                .ThenInclude(tc => tc.User)
                .Include(tc => tc.Course)
                .ThenInclude(tc => tc.Branch)
                .ToListAsync();
            return teacherCourse;
        }

        public async Task<TeacherCourse> GetTeacherCourseWithBranche(int id)
        {
            var teacherCourse = await OzelDerslerContext
                .TeachersCourses
                .Include(tc => tc.Course)
                .ThenInclude(c => c.Branch)
                .Include(tc => tc.Teacher)
                .ThenInclude(t => t.User)
                .Where(tc => tc.CourseId == id)
                .FirstOrDefaultAsync();
            return teacherCourse;
        }

        public async Task<StudentCourse> GetStudentCourseWithBranchByCourseId(int courseId)
        {
            var studentCourse = await OzelDerslerContext
                .StudentsCourses
                .Include(sc => sc.Course)
                .ThenInclude(c => c.Branch)
                .Include(sc => sc.Student)
                .ThenInclude(s => s.User)
                .Where(tc => tc.CourseId == courseId)
                .FirstOrDefaultAsync();
            return studentCourse;
        }
        public async Task<List<StudentCourse>> GetStudentCoursesByTeacherId(int studentId)
        {
            var studentCourses = await OzelDerslerContext
                .StudentsCourses
                .Include(sc => sc.Course)
                .ThenInclude(c => c.Branch)
                .Include(sc => sc.Student)
                .ThenInclude(s => s.User)
                .Where(sc => sc.StudentId == studentId)
                .ToListAsync();
            return studentCourses;
        }
    }
}
