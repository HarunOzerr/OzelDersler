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
    public class TeacherCourseConfig : IEntityTypeConfiguration<TeacherCourse>
    {
        public void Configure(EntityTypeBuilder<TeacherCourse> builder)
        {
            builder.HasKey(tc => new { tc.TeacherId, tc.CourseId });
            builder.ToTable("TeachersCourses");

            builder.HasData(
                new TeacherCourse
                {
                    CourseId = 3,
                    TeacherId = 1,
                },
                new TeacherCourse
                {
                    CourseId = 4,
                    TeacherId = 2,
                }
                );
        }
    }
}
