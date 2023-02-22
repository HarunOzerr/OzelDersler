using OzelDersler.Entity.Concrete.Identity;
using OzelDersler.Entity.Concrete;

namespace OzelDersler.Web.Areas.Admin.Models
{
    public class StudentListDto
    {
        public Student Student { get; set; }
        public User User { get; set; }
    }
}
