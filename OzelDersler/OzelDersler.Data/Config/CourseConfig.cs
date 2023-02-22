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
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasData(
                new Course
                {
                    Id = 1,
                    Name = "Matematik Özel Kursu",
                    Description = "Matematik kursu almak istiyorum.",
                    PricePerHour = 500,
                    BranchId= 1,
                    Url = "matematik-ozel-kursu"
                },
                new Course
                {
                    Id = 2,
                    Name = "Kimya Özel Kursu",
                    Description = "Kimya kursu almak istiyorum.",
                    PricePerHour = 400,
                    BranchId = 2,
                    Url = "kimya-ozel-kursu"
                },
                new Course
                {
                    Id = 3,
                    Name = "Türkçe Özel Kursu",
                    Description = "Türkçe kursu veriyorum.",
                    PricePerHour = 400,
                    BranchId = 5,
                    Url = "turkce-ozel-kursu"
                },
                new Course
                {
                    Id = 4,
                    Name = "Bilgisayar Özel Kursu",
                    Description = "Bilgisayar kursu veriyorum.",
                    PricePerHour = 500,
                    BranchId = 6,
                    Url = "bilgisayar-ozel-kursu"
                }
                );
        }
    }
}
