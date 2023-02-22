using OzelDersler.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Business.Abstract
{
    public interface IStudentService
    {
        Task<Student> GetByIdAsync(int id);
        Task<List<Student>> GetAllAsync();
        Task CreateAsync(Student student);
        void Update(Student student);
        void Delete(Student student);
        List<Student> GetStudentsWithUser();
        Task<Student> GetStudentWithUser(int id);
        Task<Student> GetStudentByUserName(string userName);
        Task<Student> GetStudentWithCourse(int id);

    }
}
