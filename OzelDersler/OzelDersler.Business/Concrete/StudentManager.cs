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
    public class StudentManager : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Student student)
        {
            await _unitOfWork.Students.CreateAsync(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _unitOfWork.Students.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _unitOfWork.Students.GetByIdAsync(id);
        }

        public void Update(Student student)
        {
            _unitOfWork.Students.Update(student);
            _unitOfWork.Save();
        }
        public void Delete(Student student)
        {
            _unitOfWork.Students.Delete(student);
            _unitOfWork.Save();
        }

        public List<Student> GetStudentsWithUser()
        {
            return _unitOfWork.Students.GetStudentsWithUser();
        }

        public async Task<Student> GetStudentWithUser(int id)
        {
            return await _unitOfWork.Students.GetStudentWithUser(id);
        }

        public async Task<Student> GetStudentByUserName(string userName)
        {
            return await _unitOfWork.Students.GetStudentByUserName(userName);
        }

        public async Task<Student> GetStudentWithCourse(int id)
        {
            return await _unitOfWork.Students.GetStudentWithCourse(id);
        }
    }
}
