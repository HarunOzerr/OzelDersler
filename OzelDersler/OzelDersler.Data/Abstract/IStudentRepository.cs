using OzelDersler.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Data.Abstract
{
    public interface IStudentRepository : IRepository<Student>
    {
        List<Student> GetStudentsWithUser();
        Task<Student> GetStudentWithUser(int id);
        Task<Student> GetStudentByUserName(string userName);
        Task<Student> GetStudentWithCourse(int id);
    }
}
