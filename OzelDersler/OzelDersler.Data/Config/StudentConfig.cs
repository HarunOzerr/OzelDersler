using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OzelDersler.Entity.Concrete;
using OzelDersler.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData
                (
                    new Student
                    {
                        Id = 1,
                        FirstName = "Harun",
                        LastName = "Özer",
                        Job = "Developer",
                        City = "İstanbul",
                        Gender = "Erkek",
                        ImageUrl = "harun.png",
                        Address = "Başakşehir",
                        Url = "harun-ozer",
                        UserId = "student"
                    },
                    new Student
                    {
                        Id = 2,
                        FirstName = "Ogrenci",
                        LastName = "Özer",
                        Job = "Developer",
                        City = "İstanbul",
                        Gender = "Erkek",
                        ImageUrl = "harun.png",
                        Address = "Başakşehir",
                        Url = "ogrenci-ozer",
                        UserId = "ogrenci"
                    }
                );
        }
    }
}
