using OzelDersler.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Data.Abstract
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<List<StudentCourse>> GetStudentCoursesWithBranches();

        Task<List<StudentCourse>> GetStudentCoursesByBranchAsync(string branchUrl);
        Task<StudentCourse> GetStudentCourseDetailsByUrlAsync(string courseUrl);

        Task<List<TeacherCourse>> GetTeacherCoursesByBranchAsync(string branchUrl);
        Task<List<TeacherCourse>> GetTeacherCoursesWithBranches();
        Task<TeacherCourse> GetTeacherCourseDetailsByUrlAsync(string courseUrl);
        Task<List<TeacherCourse>> GetTeacherCoursesByTeacherId(int id);
        Task<TeacherCourse> GetTeacherCourseWithBranche(int id);
        Task<StudentCourse> GetStudentCourseWithBranchByCourseId(int courseId);
        Task<List<StudentCourse>> GetStudentCoursesByTeacherId(int studentId);


    }
}
