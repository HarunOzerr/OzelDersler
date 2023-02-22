using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        ITeacherRepository Teachers { get; }
        IStudentRepository Students { get; }
        IBranchRepository Branches { get; }
        ICourseRepository Courses { get; }
        Task SaveAsync();
        void Save();
    }
}
