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
    public class EfCoreStudentRepository : EfCoreGenericRepository<Student>, IStudentRepository
    {
        public EfCoreStudentRepository(OzelDerslerContext context) : base(context)
        {

        }
        private OzelDerslerContext OzelDerslerContext
        {
            get { return _context as OzelDerslerContext; }
        }
        public List<Student> GetStudentsWithUser()
        {
            var students = OzelDerslerContext
                .Students
                .Include(s => s.User)
                .ToList();
            return students;
        }

        public Task<Student> GetStudentWithUser(int id)
        {
            var student = OzelDerslerContext
                .Students
                .Where(s => s.Id == id)
                .Include(s => s.User)
                .FirstOrDefaultAsync();
            return student;
        }

        public async Task<Student> GetStudentByUserName(string userName)
        {
            var student = await OzelDerslerContext
                .Students
                .Include(s => s.User)
                .Where(s => s.User.UserName == userName)
                .FirstOrDefaultAsync();
            return student;
        }

        public async Task<Student> GetStudentWithCourse(int id)
        {
            var student = await OzelDerslerContext
                .Students
                .Where(s => s.Id == id)
                .Include(s => s.User)
                .Include(s => s.StudentsCourses)
                .ThenInclude(sc => sc.Course)
                .ThenInclude(c => c.Branch)
                .FirstOrDefaultAsync();
            return student;
        }
    }
}
