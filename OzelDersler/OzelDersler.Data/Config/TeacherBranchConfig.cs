using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OzelDersler.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Data.Config
{
    public class TeacherBranchConfig : IEntityTypeConfiguration<TeacherBranch>
    {
        public void Configure(EntityTypeBuilder<TeacherBranch> builder)
        {
            builder.HasKey(tb => new {tb.TeacherId, tb.BranchId});

            builder.ToTable("TeacherBranches");

            builder.HasData(
                new TeacherBranch { BranchId = 1, TeacherId = 1 },
                new TeacherBranch { BranchId = 1, TeacherId = 2 },
                new TeacherBranch { BranchId = 2, TeacherId = 3 },
                new TeacherBranch { BranchId = 3, TeacherId = 1 },
                new TeacherBranch { BranchId = 4, TeacherId = 4 }
                );
        }
    }
}
