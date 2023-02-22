using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OzelDersler.Entity.Concrete;
using OzelDersler.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            List<User> users = new List<User>
            {
                new User
                {
                    Id = "student",
                    Email = "harun@gmail.com",
                    UserName = "student",
                    NormalizedUserName = "STUDENT"
                },
                new User
                {
                    Id ="ogrenci",
                    Email = "ogrenci@gmail.com",
                    UserName = "ogrenci",
                    NormalizedUserName = "OGRENCI"
                },
                new User
                {
                    Id = "teacher",
                    Email = "teacher@gmail.com",
                    UserName = "teacher",
                    NormalizedUserName = "TEACHER"
                },
                new User
                {
                    Id = "teacher1",
                    Email = "teacher@gmail.com",
                    UserName = "teacher1",
                    NormalizedUserName = "TEACHER1"
                },
                new User
                {
                    Id = "teacher2",
                    Email = "teacher@gmail.com",
                    UserName = "teacher2",
                    NormalizedUserName = "TEACHER2"
                },
                new User
                {
                    Id = "teacher3",
                    Email = "teacher@gmail.com",
                    UserName = "teacher3",
                    NormalizedUserName = "TEACHER3"
                },
                new User
                {
                    Id = "teacher4",
                    Email = "teacher@gmail.com",
                    UserName = "teacher4",
                    NormalizedUserName = "TEACHER4"
                },
                new User
                {
                    Id = "admin",
                    Email = "admin@hotmail.com",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN"
                }
            };
            modelBuilder.Entity<User>().HasData(users);

            var passwordHasher = new PasswordHasher<User>();
            users[7].PasswordHash = passwordHasher.HashPassword(users[7], "Qwe123.");

            List<Role> roles = new List<Role>
            {
                new Role
                {
                    Name = "Teacher",
                    Description = "Teacher Rolü",
                    NormalizedName = "TEACHER"
                },
                new Role
                {
                    Name = "Student",
                    Description = "Student Rolü",
                    NormalizedName = "STUDENT"
                },
                new Role
                {
                    Name = "Admin",
                    Description = "Admin Rolü",
                    NormalizedName = "ADMIN"
                }
            };
            modelBuilder.Entity<Role>().HasData(roles);


            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId=users[0].Id,
                    RoleId=roles.First(r=>r.Name=="Student").Id
                },
                new IdentityUserRole<string>
                {
                    UserId=users[1].Id,
                    RoleId=roles.First(r=>r.Name=="Student").Id
                },
                new IdentityUserRole<string>
                {
                    UserId=users[2].Id,
                    RoleId=roles.First(r=>r.Name=="Teacher").Id
                },
                new IdentityUserRole<string>
                {
                    UserId=users[3].Id,
                    RoleId=roles.First(r=>r.Name=="Teacher").Id
                },
                new IdentityUserRole<string>
                {
                    UserId=users[4].Id,
                    RoleId=roles.First(r=>r.Name=="Teacher").Id
                },
                new IdentityUserRole<string>
                {
                    UserId=users[5].Id,
                    RoleId=roles.First(r=>r.Name=="Teacher").Id
                },
                new IdentityUserRole<string>
                {
                    UserId=users[6].Id,
                    RoleId=roles.First(r=>r.Name=="Teacher").Id
                },
                new IdentityUserRole<string>
                {
                    UserId = users[7].Id,
                    RoleId=roles.First(r => r.Name== "Admin").Id
                }
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
    }
}
