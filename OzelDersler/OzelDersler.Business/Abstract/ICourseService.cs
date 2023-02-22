using OzelDersler.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Business.Abstract
{
    public interface ICourseService
    {
        Task<Course> GetByIdAsync(int id);
        Task<List<Course>> GetAllAsync();
        Task CreateAsync(Course course);
        void Update(Course course);
        void Delete(Course course);

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
