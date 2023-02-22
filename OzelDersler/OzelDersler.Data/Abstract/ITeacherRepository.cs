using OzelDersler.Entity.Concrete;
using OzelDersler.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Data.Abstract
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<List<Teacher>> GetTeachersByBranchAsync(string branchUrl);
        Task<List<Teacher>> GetHomePageTeachersAsync();

        Task<List<Teacher>> GetTeacherByUserId(string userId);

        User GetUser(string userName, string id);

        List<Teacher> GetTeachersWithUsers();
        
        Task CreateTeacherAsync(Teacher teacher, int[] selectedBranchId);
        Task<Teacher> GetTeacherWithBranches(int id);

        Teacher GetTeacherWithUser(int id);
        Task UpdateTeacherAsync(Teacher teacher, int[] selectedBranchIds);
        Task<Teacher> GetTeacherWithCourses(int id);
    }
}
