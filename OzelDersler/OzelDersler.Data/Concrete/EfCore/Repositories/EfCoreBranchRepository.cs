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
    public class EfCoreBranchRepository : EfCoreGenericRepository<Branch>, IBranchRepository
    {
        public EfCoreBranchRepository(DbContext context) : base(context)
        {
        }
        private OzelDerslerContext OzelDerslerContext
        {
            get { return _context as OzelDerslerContext; }
        }
    }
}
