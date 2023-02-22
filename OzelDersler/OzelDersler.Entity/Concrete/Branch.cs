using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Entity.Concrete
{
    public class Branch 
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get ; set ; }
    }
}
