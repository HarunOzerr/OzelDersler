using OzelDersler.Entity.Concrete;
using OzelDersler.Entity.Concrete.Identity;

namespace OzelDersler.Web.Areas.Admin.Models
{
    public class TeacherListDto
    {
        public Teacher Teacher { get; set; }
        public User User { get; set; }
    }
}
