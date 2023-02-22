using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Entity.Concrete
{
    public class BaseUserEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public string Job { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
