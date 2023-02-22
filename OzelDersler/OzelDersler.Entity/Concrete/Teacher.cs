using OzelDersler.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Entity.Concrete
{
    public class Teacher : BaseUserEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public bool IsHome { get; set; }
        public int PricePerHour { get; set; }
        public int Experience { get; set; }
        public string About { get; set; }
        public List<TeacherBranch> TeacherBranches { get; set; }
        public List<TeacherCourse> TeachersCourses { get; set; }
    }
}
