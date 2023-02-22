using OzelDersler.Business.Abstract;
using OzelDersler.Data.Abstract;
using OzelDersler.Entity.Concrete;
using OzelDersler.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Business.Concrete
{
    public class TeacherManager : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Teacher teacher)
        {
            await _unitOfWork.Teachers.CreateAsync(teacher);
            await _unitOfWork.SaveAsync(); 
        }

        public void Delete(Teacher teacher)
        {
            _unitOfWork.Teachers.Delete(teacher);
            _unitOfWork.Save();
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _unitOfWork.Teachers.GetAllAsync();
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            return await _unitOfWork.Teachers.GetByIdAsync(id);
        }

        public async Task<List<Teacher>> GetHomePageTeachersAsync()
        {
            return await _unitOfWork.Teachers.GetHomePageTeachersAsync();
        }

        public async Task<List<Teacher>> GetTeachersByBranchAsync(string branchUrl)
        {
            return await _unitOfWork.Teachers.GetTeachersByBranchAsync(branchUrl);
        }

        public void Update(Teacher teacher)
        {
            _unitOfWork.Teachers.Update(teacher);
            _unitOfWork.Save();
        }

        public async Task<List<Teacher>> GetTeacherByUserId(string userId)
        {
            return await _unitOfWork.Teachers.GetTeacherByUserId(userId);
        }

        public User GetUser(string userName, string id)
        {
            return _unitOfWork.Teachers.GetUser(userName, id);
        }

        public List<Teacher> GetTeachersWithUsers()
        {
            return _unitOfWork.Teachers.GetTeachersWithUsers();
        }

        public Task CreateTeacherAsync(Teacher teacher, int[] selectedBranchId)
        {
            return _unitOfWork.Teachers.CreateTeacherAsync(teacher, selectedBranchId);
        }

        public Task<Teacher> GetTeacherWithBranches(int id)
        {
            return _unitOfWork.Teachers.GetTeacherWithBranches(id);
        }

        public Teacher GetTeacherWithUser(int id)
        {
            return _unitOfWork.Teachers.GetTeacherWithUser(id);
        }

        public Task UpdateTeacherAsync(Teacher teacher, int[] selectedBranchIds)
        {
            return _unitOfWork.Teachers.UpdateTeacherAsync(teacher, selectedBranchIds);
        }

        public async Task<Teacher> GetTeacherWithCourses(int id)
        {
            return await _unitOfWork.Teachers.GetTeacherWithCourses(id);
        }
    }
}
