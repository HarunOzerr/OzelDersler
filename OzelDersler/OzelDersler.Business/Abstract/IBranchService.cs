using OzelDersler.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Business.Abstract
{
    public interface IBranchService
    {
        Task<Branch> GetByIdAsync(int id);
        Task<List<Branch>> GetAllAsync();
        Task CreateAsync(Branch branch);
        void Update(Branch branch);
        void Delete(Branch branch);
        Branch GetByIdWithTeachers();
    }
}
