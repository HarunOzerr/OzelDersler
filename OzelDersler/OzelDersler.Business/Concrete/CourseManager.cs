using OzelDersler.Business.Abstract;
using OzelDersler.Data.Abstract;
using OzelDersler.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Business.Concrete
{
    public class CourseManager : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Course course)
        {
            await _unitOfWork.Courses.CreateAsync(course);
            await _unitOfWork.SaveAsync();
        }
        public void Delete(Course course)
        {
            _unitOfWork.Courses.Delete(course);
            _unitOfWork.Save();
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _unitOfWork.Courses.GetAllAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _unitOfWork.Courses.GetByIdAsync(id);
        }

        public async Task<StudentCourse> GetStudentCourseDetailsByUrlAsync(string courseUrl)
        {
            return await _unitOfWork.Courses.GetStudentCourseDetailsByUrlAsync(courseUrl);
        }

        public async Task<List<StudentCourse>> GetStudentCoursesByBranchAsync(string branchUrl)
        {
            return await _unitOfWork.Courses.GetStudentCoursesByBranchAsync(branchUrl);
        }

        public async Task<List<StudentCourse>> GetStudentCoursesWithBranches()
        {
            return await _unitOfWork.Courses.GetStudentCoursesWithBranches();
        }

        public async Task<StudentCourse> GetStudentCourseWithBranchByCourseId(int courseId)
        {
            return await _unitOfWork.Courses.GetStudentCourseWithBranchByCourseId(courseId);
        }

        public async Task<List<TeacherCourse>> GetTeacherCoursesByTeacherId(int id)
        {
            return await _unitOfWork.Courses.GetTeacherCoursesByTeacherId(id);
        }

        public async Task<TeacherCourse> GetTeacherCourseDetailsByUrlAsync(string courseUrl)
        {
            return await _unitOfWork.Courses.GetTeacherCourseDetailsByUrlAsync(courseUrl);
        }

        public async Task<List<TeacherCourse>> GetTeacherCoursesByBranchAsync(string branchUrl)
        {
            return await _unitOfWork.Courses.GetTeacherCoursesByBranchAsync(branchUrl);
        }

        public async Task<List<TeacherCourse>> GetTeacherCoursesWithBranches()
        {
            return await _unitOfWork.Courses.GetTeacherCoursesWithBranches();
        }

        public async Task<TeacherCourse> GetTeacherCourseWithBranche(int id)
        {
            return await _unitOfWork.Courses.GetTeacherCourseWithBranche(id);
        }

        public void Update(Course course)
        {
            _unitOfWork.Courses.Update(course);
            _unitOfWork.Save();
        }

        public async Task<List<StudentCourse>> GetStudentCoursesByTeacherId(int studentId)
        {
            return await _unitOfWork.Courses.GetStudentCoursesByTeacherId(studentId);
        }
    }
}
