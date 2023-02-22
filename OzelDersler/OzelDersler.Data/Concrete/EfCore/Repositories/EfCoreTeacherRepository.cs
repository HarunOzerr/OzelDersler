using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OzelDersler.Data.Abstract;
using OzelDersler.Data.Concrete.EfCore.Contexts;
using OzelDersler.Entity.Concrete;
using OzelDersler.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Data.Concrete.EfCore.Repositories
{
    public class EfCoreTeacherRepository : EfCoreGenericRepository<Teacher>, ITeacherRepository
    {
        public EfCoreTeacherRepository(OzelDerslerContext context) : base(context)
        {

        }
        private OzelDerslerContext OzelDerslerContext
        {
            get { return _context as OzelDerslerContext; }
        }
        public async Task<List<Teacher>> GetHomePageTeachersAsync()
        {
            return await OzelDerslerContext.Teachers.Where(t => t.IsHome == true).ToListAsync();
        }

        public async Task<List<Teacher>> GetTeachersByBranchAsync(string branchUrl)
        {
            var teachers = OzelDerslerContext
                .Teachers
                .AsQueryable();
            if (branchUrl != null)
            {
                teachers =
                    teachers.Include(t => t.TeacherBranches)
                    .ThenInclude(tb => tb.Branch)
                    .Where(t => t.TeacherBranches.Any(tb => tb.Branch.Url == branchUrl));
            }
            return await teachers.ToListAsync();
        }

        public async Task<List<Teacher>> GetTeacherByUserId(string userId)
        {
            var teacher = await OzelDerslerContext
                .Teachers
                .Include(t => t.User)
                .Where(tu => tu.User.Id == userId).ToListAsync();

            return teacher;
        }

        public User GetUser(string userName, string id)
        {
            var user = OzelDerslerContext
                .Users
                .Include(t => t.Teachers)
                .Include(t => t.Students)
                .FirstOrDefault(u => u.UserName == userName || u.Id == id);
            return user;
        }

        public List<Teacher> GetTeachersWithUsers()
        {
            var teachers = OzelDerslerContext
                .Teachers
                .Include(t => t.User)
                .ToList();
            return teachers;
        }

        public Teacher GetTeacherWithUser(int id)
        {
            var teacher = OzelDerslerContext
                .Teachers
                .Where(t => t.Id == id)
                .Include(t => t.User)
                .FirstOrDefault();
            return teacher;
        }

        public async Task CreateTeacherAsync(Teacher teacher, int[] selectedBranchId)
        {
            await OzelDerslerContext.Teachers.AddAsync(teacher);
            await OzelDerslerContext.SaveChangesAsync();
            teacher.TeacherBranches = selectedBranchId
                .Select(brId => new TeacherBranch
                {
                    TeacherId = teacher.Id,
                    BranchId = brId
                }).ToList();
            await OzelDerslerContext.SaveChangesAsync();
        }

        public async Task<Teacher> GetTeacherWithBranches(int id)
        {
            return await OzelDerslerContext
                .Teachers
                .Where(t => t.Id == id)
                .Include(t => t.TeacherBranches)
                .ThenInclude(tb => tb.Branch)
                .Include(t => t.User)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateTeacherAsync(Teacher teacher, int[] selectedBranchIds)
        {
            var newTeacher = await OzelDerslerContext
               .Teachers
               .Include(t => t.TeacherBranches)
               .FirstOrDefaultAsync(t => t.Id == teacher.Id);
            newTeacher.TeacherBranches = selectedBranchIds
                .Select(brId => new TeacherBranch
                {
                    TeacherId = newTeacher.Id,
                    BranchId = brId
                }).ToList();
            OzelDerslerContext.Update(newTeacher);
            await OzelDerslerContext.SaveChangesAsync();
        }

        public async Task<Teacher> GetTeacherWithCourses(int id)
        {
            var teacher = await OzelDerslerContext
                .Teachers
                .Where(t => t.Id == id)
                .Include(t => t.TeachersCourses)
                .ThenInclude(t => t.Course)
                .Include(t => t.User)
                .FirstOrDefaultAsync();
            return teacher;
        }
    }
}
