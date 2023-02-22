using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Entity.Concrete.Identity
{
    public class User : IdentityUser
    {
        public Student Students { get; set; }
        public Teacher Teachers { get; set; }
    }
}
