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
    public class StudentCourseConfig : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(sc => new { sc.StudentId, sc.CourseId });
            builder.ToTable("StudentsCourses");

            builder.HasData(
                new StudentCourse
                {
                    CourseId = 1,
                    StudentId = 1
                },
                new StudentCourse
                {
                    CourseId= 2,
                    StudentId = 2
                }
                );
        }
    }
}
